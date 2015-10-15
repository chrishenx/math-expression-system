//
//  CGLOperand.m
//  math-expressions-system
//
//  Created by christian gonzalez on 15/10/15.
//  Copyright © 2015 chrishenx. All rights reserved.
//

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
