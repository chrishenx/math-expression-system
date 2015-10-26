//=======================================================================
//  CGLNonTerminalSymbol.m
//  math-expressions-system/objective-c
//  Created by christian gonzalez on 15/10/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================

#import "CGLNonTerminalSymbol.h"


@implementation CGLNTerminalSymbolD

+ (NSString*) description: (CGLNTerminalSymbol) nTerminalSymbol {
    return [NSString stringWithFormat:@"%c", (char) ((int) 'A' + nTerminalSymbol)];
}

@end
