//
//  CGLSymbol.h
//  math-expressions-system
//
//  Created by christian gonzalez on 15/10/15.
//  Copyright © 2015 chrishenx. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "CGLTerminalSymbol.h"
#import "CGLNonTerminalSymbol.h"

@interface CGLSymbol : NSObject

@property (readonly) BOOL isTerminal;
@property (readonly) CGLTerminalSymbolType terminalSymbol;
@property (readonly) CGLNTerminalSymbol nonTerminalSymbol;

- (id) initWithTSymbol: (CGLTerminalSymbolType) terminalSymbol;
- (id) initWithNTSymbol: (CGLNTerminalSymbol) nonTerminalSymbol;

@end
