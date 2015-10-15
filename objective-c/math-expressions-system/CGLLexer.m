//
//  CGLLexer.m
//  math-expressions-system
//
//  Created by christian gonzalez on 15/10/15.
//  Copyright © 2015 chrishenx. All rights reserved.
//

#import "CGLLexer.h"

#define END_OF_EXPR ' '

typedef NS_ENUM(NSInteger, CGLState) {
    CGLBegin,
    CGLVar,
    CGLFunc,
    CGLNum1,
    CGLTrans,
    CGLNum2
};

@implementation CGLLexer

+ (void) initFunctions {
    FUNCTIONS = @[
        @"sin",  @"cos",   @"tan",  @"arcsin", @"arccos", @"arctan", // Trigonometric
        @"sec",  @"cosec", @"cotan",
        @"sinh", @"cosh",  @"tanh", // Hyperbolic
        @"exp",  @"ln",    @"log",  @"sqrt",   @"abs",    @"cbrt",  @"sinc", @"hstep" // Miscellaneous
    ];
}

- (id) initWithExpression: (NSString*) expression {
    if (self = [super init]) {
        _expression = expression;
        _lexicalError = false;
    }
    return self;
}

- (NSMutableArray*) generateTokens {
    const NSUInteger N = [_expression length];
    NSMutableArray* tokens = [[NSMutableArray alloc] initWithCapacity:N * 2];
    NSMutableString* tokenValue = [[NSMutableString alloc] initWithCapacity:N * 2];
    CGLState currentState = CGLBegin;
    NSUInteger position = 0;
    while (position <= N && !_lexicalError) {
        
    }
    return nil; // TODO Change when the implementation is ready
}

+ (CGLToken*) makeOneCharToken:(nonnull NSString*) symbol
                    withColumn:(int) column{
    if ([@"+-*/^" rangeOfString:symbol].location != NSNotFound) {
        return [[CGLToken alloc] initSymbol:CGLOperator withDetails:column :symbol];
    } else if ([symbol isEqualToString:@"("]) {
        return [[CGLToken alloc] initSymbol:CGLOpeningParenthesis withDetails:column :symbol];
    } else if ([symbol isEqualToString:@"("]) {
        return [[CGLToken alloc] initSymbol:CGLClosingParenthesis withDetails:column :symbol];
    }
    return nil;
}

+ (BOOL) isFunction:(NSString*) str {
    return [FUNCTIONS containsObject:str];
}

@end
