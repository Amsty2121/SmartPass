package com.example.smartpassuserdevice.data.repository

import com.example.smartpassuserdevice.data.model.LoginRequest
import com.example.smartpassuserdevice.data.model.LoginResponse
import com.example.smartpassuserdevice.ui.apiFunctionality.ApiServiceFactory
import retrofit2.Response

class UserRepository(private val apiServiceFactory: ApiServiceFactory) {

    suspend fun login(loginRequest: LoginRequest): Response<LoginResponse> {
        val apiService = apiServiceFactory.getApiService()
        return apiService.loginUser(loginRequest)
    }

    suspend fun refreshToken(token: String): Response<LoginResponse> {
        val apiService = apiServiceFactory.getApiService()
        return apiService.refreshToken(token)
    }
}