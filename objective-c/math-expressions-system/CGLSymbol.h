//=======================================================================
//  CGLSymbol.h
//  math-expressions-system/objective-c
//  Created by christian gonzalez on 15/10/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================


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
