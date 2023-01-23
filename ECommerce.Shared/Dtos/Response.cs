﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccessful { get;  private set; }
        public List<string>Errors { get; private set; }
        public string Error { get; private set; }


        public static Response<T> Success(T data,int statusCode) 
        {
            return new Response<T> { Data = data, StatusCode = statusCode,IsSuccessful=true };
        }
        public static Response<T> Success(int statusCode) 
        { return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true }; }

        public static Response<T> Fail (List<string> errors,int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }
        public static Response<T> Fail (string error,int statusCode)
        {
            return new Response<T> { Error = error, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
