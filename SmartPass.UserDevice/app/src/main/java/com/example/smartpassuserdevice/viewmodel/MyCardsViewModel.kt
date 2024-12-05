package com.example.smartpassuserdevice.viewmodel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.viewModelScope
import com.example.smartpassuserdevice.data.model.AccessCard
import com.example.smartpassuserdevice.data.model.GetMyAccessCardsMobileRsp
import com.example.smartpassuserdevice.data.repository.CardsRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.launch

class MyCardsViewModel(private val cardsRepository: CardsRepository) : ViewModel() {

    // Используем StateFlow для отслеживания списка карт
    private val _cardsState = MutableStateFlow<List<AccessCard>>(emptyList())
    val cardsState: StateFlow<List<AccessCard>> = _cardsState

    // Функция для получения карт
    suspend fun getMyCards(token: String): Result<GetMyAccessCardsMobileRsp?> {
        val response = cardsRepository.getMyCards(token)

        return if (response.isSuccessful) {
            Result.success(response.body())
        } else {
            Result.success(null)
        }
    }

    // Обновляем состояние карт
    fun updateCardsState(cards: List<AccessCard>) {
        _cardsState.value = cards
    }

    // Функция для загрузки карт
    fun fetchMyCards(token: String) {
        viewModelScope.launch {
            val result = getMyCards(token)
            if (result.isSuccess) {
                val response = result.getOrNull()
                response?.let {
                    updateCardsState(it.accessCards)
                }
            }
        }
    }

    class MyCardsViewModelFactory(
        private val repository: CardsRepository
    ) : ViewModelProvider.Factory {

        override fun <T : ViewModel> create(modelClass: Class<T>): T {
            if (modelClass.isAssignableFrom(MyCardsViewModel::class.java)) {
                @Suppress("UNCHECKED_CAST")
                return MyCardsViewModel(repository) as T
            }
            throw IllegalArgumentException("Unknown ViewModel class")
        }
    }
}
