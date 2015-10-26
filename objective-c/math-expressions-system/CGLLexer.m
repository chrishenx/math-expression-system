//=======================================================================
//  CGLLexer.m
//  math-expressions-system/objective-c
//  Created by christian gonzalez on 15/10/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================

#import "CGLLexer.h"
#import <stdlib.h>

#define CGL_END_OF_EXPR ' '

NSArray* FUNCTIONS;

typedef NS_ENUM(NSInteger, CGLState)
{
    CGLStateBegin,
    CGLStateVar,
    CGLStateFunc,
    CGLStateOk,
    CGLStateNum1,
    CGLStateTrans,
    CGLStateNum2
};

BOOL CGLIsOperator(unichar symbol)
{
    return symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/' || symbol == '^';
}

BOOL CGLIsParenthesis(unichar symbol)
{
    return symbol == '(' || symbol == ')';
}

NSString* unichar2nsstring(unichar symbol)
{
    return [NSString stringWithFormat:@"%c", symbol];
}

@implementation CGLLexer

+ (void) initFunctions
{
    FUNCTIONS = @[
      @"sin",  @"cos",   @"tan",  @"arcsin", @"arccos", @"arctan", // Trigonometric
      @"sec",  @"cosec", @"cotan",
      @"sinh", @"cosh",  @"tanh", // Hyperbolic
      @"exp",  @"ln",    @"log",  @"sqrt",   @"abs",    @"cbrt",  @"sinc", @"hstep" // Miscellaneous
      ];
}

- (id) initWithExpression: (NSString*) expression
{
    if (self = [super init])
    {
        _expression = expression;
        _lexicalError = false;
    }
    return self;
}

- (NSMutableArray*) generateTokens
{
    const NSUInteger N = [_expression length];
    NSMutableArray* tokens = [[NSMutableArray alloc] initWithCapacity:N * 2];
    NSMutableString* tokenValue = [[NSMutableString alloc] initWithCapacity:N * 2];
    CGLState currentState = CGLStateBegin;
    NSUInteger position = 0;
    while (position <= N && !_lexicalError)
    {
        unichar symbol = position < N ? [_expression characterAtIndex:position]: CGL_END_OF_EXPR;
        switch (currentState)
        {
            case CGLStateBegin:
                if (isalpha(symbol))
                {
                    [tokenValue appendFormat:@"%C", symbol];
                    currentState = CGLStateVar;
                }
                else if (isdigit(symbol))
                {
                    [tokenValue appendFormat:@"%C", symbol];
                    currentState = CGLStateNum1;
                }
                else if (CGLIsOperator(symbol) || CGLIsParenthesis(symbol))
                {
                    [tokenValue appendFormat:@"%C", symbol];
                    currentState = CGLStateOk;
                }
                else if (symbol != CGL_END_OF_EXPR)
                {
                    if (position < N)
                    {
                        NSString* error = [NSString stringWithFormat:@"Invalid symbol %C", symbol];
                        _tokenError = [[CGLToken alloc] initSymbol:CGLUnRecognized withDetails:position :error];
                        [tokens addObject:_tokenError];
                        _lexicalError = true;
                    }
                }
                position++;
                break;
            case CGLStateVar:
                if (isalpha(symbol))
                {
                    [tokenValue appendFormat:@"%C", symbol];
                    position++;
                    currentState = CGLStateFunc;
                }
                else
                {
                    CGLToken* newToken = [[CGLToken alloc] initSymbol:CGLVariable withDetails:position :[NSString stringWithString:tokenValue]];
                    [tokens addObject:newToken];
                    [tokenValue setString:@""];
                    currentState = CGLStateBegin;
                }
                break;
            case CGLStateFunc:
                if (isalpha(symbol))
                {
                    [tokenValue appendFormat:@"%C", symbol];
                    position++;
                }
                else
                {
                    NSString* str = [NSString stringWithString:tokenValue];
                    if ([CGLLexer isFunction: str])
                    {
                        CGLToken* newToken = [[CGLToken alloc] initSymbol:CGLFunction withDetails:position :str];
                        [tokens addObject:newToken];
                        [tokenValue setString:@""];
                        currentState = CGLStateBegin;
                    }
                    else
                    {
                        NSString* error = [NSString stringWithFormat:@"Unrecognized %@", str];
                        _tokenError = [[CGLToken alloc] initSymbol:CGLUnRecognized withDetails:position :error];
                        [tokens addObject:_tokenError];
                        _lexicalError = YES;
                    }
                }
                break;
            case CGLStateOk:
                [tokens addObject:[CGLLexer makeOneCharToken:symbol withColumn:position]];
                [tokenValue setString:@""];
                currentState = CGLStateBegin;
                break;
            case CGLStateNum1:
                if (isdigit(symbol))
                {
                    [tokenValue appendFormat:@"%C", symbol];
                    position++;
                }
                else if (symbol == '.')
                {
                    [tokenValue appendFormat:@"%C", symbol];
                    position++;
                    currentState = CGLStateTrans;
                }
                else
                {
                    NSString* str = [NSString stringWithString:tokenValue];
                    CGLToken* newToken = [[CGLToken alloc] initSymbol:CGLValue withDetails:position :str];
                    [tokens addObject:newToken];
                    [tokenValue setString:@""];
                    currentState = CGLStateBegin;
                }
                break;
            case CGLStateTrans:
                if (isdigit(symbol))
                {
                    [tokenValue appendFormat:@"%C", symbol];
                    position++;
                    currentState = CGLStateNum2;
                }
                else
                {
                    [tokenValue appendFormat:@"%C", symbol]; // Complete bad formated number
                    NSString* error = [NSString stringWithFormat:@"Invalid number format %@", tokenValue];
                    _tokenError = [[CGLToken alloc] initSymbol:CGLUnRecognized withDetails:position :error];
                    [tokens addObject:_tokenError];
                    _lexicalError = YES;
                }
                break;
            case CGLStateNum2:
                if (isdigit(symbol))
                {
                    [tokenValue appendFormat:@"%C", symbol];
                    position++;
                }
                else
                {
                    NSString* str = [NSString stringWithString:tokenValue];
                    CGLToken* newToken = [[CGLToken alloc] initSymbol:CGLValue withDetails:position :str];
                    [tokens addObject:newToken];
                    [tokenValue setString:@""];
                    currentState = CGLStateBegin;
                }
                break;
        }
    }
    return nil; // TODO Change when the implementation is ready
}

+ (CGLToken*) makeOneCharToken:(unichar) symbol
                    withColumn:(NSUInteger) column
{
    if (CGLIsOperator(symbol))
    {
        return [[CGLToken alloc] initSymbol:CGLOperator withDetails:column :unichar2nsstring(symbol)];
    }
    else if (symbol == '(')
    {
        return [[CGLToken alloc] initSymbol:CGLOpeningParenthesis withDetails:column :unichar2nsstring(symbol)];
    }
    else if (symbol == ')')
    {
        return [[CGLToken alloc] initSymbol:CGLClosingParenthesis withDetails:column :unichar2nsstring(symbol)];
    }
    return nil;
}

+ (BOOL) isFunction:(NSString*) str
{
    return [FUNCTIONS containsObject:str];
}

@end
