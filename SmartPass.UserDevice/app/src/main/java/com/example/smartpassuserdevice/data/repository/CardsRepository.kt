package com.example.smartpassuserdevice.data.repository

import com.example.smartpassuserdevice.data.model.GetMyAccessCardsMobileResponse
import com.example.smartpassuserdevice.ui.apiFunctionality.ApiServiceFactory
import retrofit2.Response

class CardsRepository( private  val apiServiceFactory: ApiServiceFactory) {

    suspend fun getMyCards(token: String): Response<GetMyAccessCardsMobileResponse> {
        val apiService = apiServiceFactory.getApiService()
        return apiService.getMyCards(token)
    }
}
