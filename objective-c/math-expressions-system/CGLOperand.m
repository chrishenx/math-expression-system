//=======================================================================
//  CGLOperand.m
//  math-expressions-system/objective-c
//  Created by christian gonzalez on 15/10/15.
//
//  Copyright © Christian González 2015.
//
//  Distributed under the MIT License.
//  (See accompanying file LICENSE or copy at
//  http://opensource.org/licenses/MIT)
//=======================================================================

#import "CGLOperand.h"

@implementation CGLOperand

- (id) initOperand {
    return [self initOperand:NO withValue:@"0"];
}

- (id) initOperand:(BOOL) isValue
         withValue:(NSString*) value {
    if (self = [super init]) {
        _isValue = isValue;
        _value = value;
    }
    return self;
}

@end
