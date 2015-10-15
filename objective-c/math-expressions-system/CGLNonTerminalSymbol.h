//
//  CGLNonTerminalSymbol.h
//  math-expressions-system
//
//  Created by christian gonzalez on 15/10/15.
//  Copyright © 2015 chrishenx. All rights reserved.
//

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
