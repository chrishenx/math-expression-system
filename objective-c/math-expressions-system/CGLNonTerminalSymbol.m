//
//  CGLNonTerminalSymbol.m
//  math-expressions-system
//
//  Created by christian gonzalez on 15/10/15.
//  Copyright © 2015 chrishenx. All rights reserved.
//

#import "CGLNonTerminalSymbol.h"


@implementation CGLNTerminalSymbolD

+ (NSString*) description: (CGLNTerminalSymbol) nTerminalSymbol {
    return [NSString stringWithFormat:@"%c", (char) ((int) 'A' + nTerminalSymbol)];
}

@end
