/*
 *  Copyright 2014 The WebRTC Project Authors. All rights reserved.
 *
 *  Use of this source code is governed by a BSD-style license
 *  that can be found in the LICENSE file in the root of the source
 *  tree. An additional intellectual property rights grant can be found
 *  in the file PATENTS.  All contributing project authors may
 *  be found in the AUTHORS file in the root of the source tree.
 */

#import "ARDAppClient.h"

#import "WebRTC/RTCPeerConnection.h"

#import "ARDRoomServerClient.h"
#import "ARDSignalingChannel.h"
#import "ARDTURNClient.h"

@class RTCPeerConnectionFactory;

@interface ARDAppClient () <ARDSignalingChannelDelegate,
  RTCPeerConnectionDelegate>

// All properties should only be mutated from the main queue.
@property(nonatomic, strong) id<ARDRoomServerClient> roomServerClient;
@property(nonatomic, strong) id<ARDSignalingChannel> channel;
@property(nonatomic, strong) id<ARDSignalingChannel> loopbackChannel;
@property(nonatomic, strong) id<ARDTURNClient> turnClient;

@property(nonatomic, str