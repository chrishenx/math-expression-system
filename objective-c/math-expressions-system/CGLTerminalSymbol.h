//
//  CGLTerminalSymbol.h
//  complex_numbers
//
//  Created by christian gonzalez on 14/10/15.
//  Copyright © 2015 chrishenx. All rights reserved.
//

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
