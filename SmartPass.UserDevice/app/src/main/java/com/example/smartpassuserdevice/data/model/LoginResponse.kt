package com.example.smartpassuserdevice.data.model

data class LoginResponse(
    val token: String,
    val refreshToken: String
)