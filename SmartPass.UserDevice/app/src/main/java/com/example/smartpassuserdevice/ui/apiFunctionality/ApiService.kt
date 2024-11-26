package com.example.smartpassuserdevice.ui.apiFunctionality

import com.example.smartpassuserdevice.data.model.GetMyAccessCardsMobileResponse
import com.example.smartpassuserdevice.data.model.LoginRequest
import com.example.smartpassuserdevice.data.model.LoginResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.Query

interface ApiService {
    @POST("User/Mobile/Login")
    suspend fun loginUser(@Body request: LoginRequest): Response<LoginResponse>

    @POST("User/Mobile/RefreshToken")
    suspend fun refreshToken(@Query("token") token: String): Response<LoginResponse>

    @GET("AccessCard/Mobile/MyCards")
    suspend fun getMyCards(@Header("Authorization") token: String) : Response<GetMyAccessCardsMobileResponse>
}