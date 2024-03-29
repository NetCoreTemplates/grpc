﻿using System.Runtime.Serialization;
using ServiceStack;

namespace MyApp.ServiceModel;

/**
 * gRPC uses protobuf-net which requires [DataContract] / [DataMember(Order=N)] attributes on all DTOs
 * https://github.com/protobuf-net/protobuf-net/wiki/Attributes
 *
 * Request DTOs should implement IReturn<T> or IReturnVoid
 * 
 * ServiceStack's Structured Error Responses requires a ResponseStatus property in Response DTOs
 * and throws WebServiceException in GrpcServiceClient
 */
[Route("/hello/{Name}")]
[DataContract]
public class Hello : IGet, IReturn<HelloResponse>
{
    [DataMember(Order = 1)]
    public string Name { get; set; }
}

[DataContract]
public class HelloResponse
{
    [DataMember(Order = 1)]
    public string Result { get; set; }
        
    [DataMember(Order = 2)]
    public ResponseStatus ResponseStatus { get; set; }
}