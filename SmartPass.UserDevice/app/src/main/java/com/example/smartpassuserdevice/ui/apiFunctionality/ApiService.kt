package com.example.smartpassuserdevice.ui.apiFunctionality

import com.example.smartpassuserdevice.data.model.GetMyAccessCardsMobileRsp
import com.example.smartpassuserdevice.data.model.LoginRequest
import com.example.smartpassuserdevice.data.model.LoginResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.Query

interface ApiService {
    @POST("Login")
    suspend fun loginUser(@Body request: LoginRequest):
            Response<LoginResponse>

    @GET("RefreshToken")
    suspend fun refreshToken(@Query("token") token: String):
            Response<LoginResponse>

    @GET("MyCards")
    suspend fun getMyCards(@Header("Authorization") token: String):
            Response<GetMyAccessCardsMobileRsp>

    @GET("TokenVerify")
    suspend fun tokenVerify(@Header("Authorization") token: String):
            Response<Unit>
}