﻿class EmptyResultConverter :
    ResultConverter<EmptyResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, EmptyResult result)
    {
    }
}