//=======================================================================
//  CGLTerminalSymbol.h
//  math-expressions-system/objective-c
//  Created by christian gonzalez on 14/10/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, CGLTerminalSymbolType) {
    CGLVariable,
    CGLValue,
    CGLOperator,
    CGLFunction,
    CGLOpeningParenthesis,
    CGLClosingParenthesis,
    CGLUnRecognized
};

@interface CGLTerminalSymbol : NSObject

@property (readonly) CGLTerminalSymbolType type;
@property (readonly) NSUInteger column;
@property (readonly) NSString* value;

- (id) initSymbol:(CGLTerminalSymbolType) type;
- (id) initSymbol:(CGLTerminalSymbolType) type
      withDetails:(NSUInteger) column : (NSString*) value;

- (NSString*) description;

- (BOOL) isEqual:(CGLTerminalSymbol*)other;

- (NSUInteger) hash;

@end
