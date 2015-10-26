//=======================================================================
//  CGLSymbol.m
//  math-expressions-system/objective-c
//  Created by christian gonzalez on 15/10/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================

#import "CGLSymbol.h"


@implementation CGLSymbol

- (id) initWithTSymbol: (CGLTerminalSymbolType) terminalSymbol {
    if (self = [super init]) {
        _terminalSymbol = terminalSymbol;
    }
    return self;
}

- (id) initWithNTSymbol: (CGLNTerminalSymbol) nonTerminalSymbol {
    if (self = [super init]) {
        _nonTerminalSymbol = nonTerminalSymbol;
    }
    return self;
}

@end
