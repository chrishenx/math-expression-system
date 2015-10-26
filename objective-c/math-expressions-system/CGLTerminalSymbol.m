//=======================================================================
//  CGLTerminalSymbol.m
//  math-expressions-system/objective-c
//  Created by christian gonzalez on 14/10/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================

#import "CGLTerminalSymbol.h"



@implementation CGLTerminalSymbol

- (id) initSymbol:(CGLTerminalSymbolType) type {
    return [self initSymbol:type withDetails:0 :nil];
}

- (id) initSymbol:(CGLTerminalSymbolType)type withDetails:(NSUInteger)column :(NSString *)value {
    if (self = [super init]) {
        _type = type;
        _column = column;
        _value = value;
    }
    return self;
}

- (NSString*) description {
    switch (_type) {
        case CGLVariable:
            return [NSString stringWithFormat:@"Variable '%@'", _value];
            break;
        case CGLValue:
            return [NSString stringWithFormat:@"Value '%@'", _value];
            break;
        case CGLOperator:
            return [NSString stringWithFormat:@"Operator '%@'", _value];
            break;
        case CGLFunction:
            return [NSString stringWithFormat:@"Function '%@'", _value];
            break;
        case CGLOpeningParenthesis:
            return @"Opening parenthesis";
            break;
        case CGLClosingParenthesis:
            return @"Closing parenthesis";
            break;
        case CGLUnRecognized:
            return [NSString stringWithFormat:@"Unrecognized '%@'", _value];
            break;
            
    }
    return nil;
}

//- (BOOL) isEqualToSymbol:(CGXTerminalSymbol*)

- (BOOL) isEqual:(id)other {
    if (self == other)
        return YES;
    if ([other isKindOfClass: [CGLTerminalSymbol class]]) {
        return [(CGLTerminalSymbol *)other type] == [self type];
    }
    //if ([other isKind])
    return NO;
}

- (NSUInteger) hash {
    return (NSUInteger) _type;
}

@end
