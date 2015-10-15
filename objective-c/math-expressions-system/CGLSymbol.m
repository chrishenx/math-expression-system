//
//  CGLSymbol.m
//  math-expressions-system
//
//  Created by christian gonzalez on 15/10/15.
//  Copyright © 2015 chrishenx. All rights reserved.
//

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
