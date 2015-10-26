//=======================================================================
//  CGLNonTerminalSymbol.h
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

typedef NS_ENUM(NSInteger, CGLNTerminalSymbol) {
    A, B, C, D, E, F, G, H, I, J, K, L, M, N,
    O, P, Q, R, S, T, U, V, W, X, Y, Z
};

/**
 
 */
@interface CGLNTerminalSymbolD : NSObject

+ (NSString*) description: (CGLNTerminalSymbol) nTerminalSymbol;

@end
