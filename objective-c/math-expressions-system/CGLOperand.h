//=======================================================================
//  CGLOperand.h
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

@interface CGLOperand : NSObject

@property (readonly) BOOL isValue; // Otherwise is an address
@property (readonly) NSString* value;

- (id) initOperand;
- (id) initOperand:(BOOL) isValue
         withValue:(NSString*) value;

@end
