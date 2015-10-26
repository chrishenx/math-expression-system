//=======================================================================
//  CGLLexer.h
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

typedef CGLTerminalSymbol CGLToken;
typedef CGLTerminalSymbolType CGLTokenType;

extern NSArray* FUNCTIONS; // Nobody is supposed to change the values in here.

/*
 Automaton as tokens generator and patterns validator
 
 Z = {A,B,...,Z} U {a,b,...,z} U {0,1,...,9} U {+,-,*,^,(,)}
 states = {BEGIN, VAR, FUNC, NUM1, TRANS, NUM2, OK}
 aceptors = {VAR, FUNC, NUM1, NUM2, OK}
 
 | [Aa-Zz] |  [0-9] |    .    |  +,-,*,^,(,)  |
 BEGIN  |  VAR    |  NUM1  |         |      OK       |
 VAR    |  FUNC   |        |         |               |
 FUNC   |  FUNC   |        |         |               |
 NUM1   |         |  NUM1  |  TRANS  |               |
 TRANS  |         |  NUM2  |         |               |
 NUM2   |         |  NUM2  |         |               |
 OK     |         |        |         |               |
 
 All the aceptors return to BEGIN when there are not a transition with
 the symbol currently read, and that symbol is not consumed. So, the
 complexity of the analisys is at most F(2n) -> O(n)
 
 @author Christian González León
 */
@interface CGLLexer : NSObject

@property NSString* expression;
@property CGLToken* tokenError;
@property BOOL lexicalError;

+ (void) initFunctions;

- (id) initWithExpression: (NSString*) expression;

- (NSMutableArray*) generateTokens;


@end
