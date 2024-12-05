package com.example.smartpassuserdevice.viewmodel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import com.example.smartpassuserdevice.data.model.LoginRequest
import com.example.smartpassuserdevice.data.model.LoginResponse
import com.example.smartpassuserdevice.data.repository.UserRepository

class UserViewModel(private val userRepository: UserRepository) : ViewModel() {

    suspend fun login(loginRequest: LoginRequest): Result<LoginResponse?> {
        val response = userRepository.login(loginRequest)

        return if (response.isSuccessful) {
            Result.success(response.body())
        } else {
            Result.success(null)
        }
    }

    suspend fun refreshToken(token: String): Result<LoginResponse?> {
        val response = userRepository.refreshToken(token)

        return if (response.isSuccessful) {
            Result.success(response.body())
        } else {
            Result.success(null)
        }
    }

    suspend fun tokenVerify(token: String): Result<Boolean> {
        val response = userRepository.tokenVerify(token)

        return if (response.isSuccessful) {
            Result.success(true) // Токен валиден
        } else {
            Result.success(false) // Токен невалиден (например, 401)
        }
    }

    class LoginViewModelFactory(
        private val repository: UserRepository
    ) : ViewModelProvider.Factory {

        override fun <T : ViewModel> create(modelClass: Class<T>): T {
            if (modelClass.isAssignableFrom(UserViewModel::class.java)) {
                @Suppress("UNCHECKED_CAST")
                return UserViewModel(repository) as T
            }
            throw IllegalArgumentException("Unknown ViewModel class")
        }
    }
}


