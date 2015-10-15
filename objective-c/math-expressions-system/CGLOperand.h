//
//  CGLOperand.h
//  math-expressions-system
//
//  Created by christian gonzalez on 15/10/15.
//  Copyright © 2015 chrishenx. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CGLOperand : NSObject

@property (readonly) BOOL isValue; // Otherwise is an address
@property (readonly) NSString* value;

- (id) initOperand;
- (id) initOperand:(BOOL) isValue
         withValue:(NSString*) value;

@end
