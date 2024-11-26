package com.example.smartpassuserdevice.ui.view

import CacheUtils
import android.content.Context
import android.util.Log
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyRow
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.compose.ui.platform.LocalContext
import androidx.lifecycle.ViewModelStoreOwner
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavHostController
import com.example.smartpassuserdevice.data.model.AccessCard
import com.example.smartpassuserdevice.data.repository.CardsRepository
import com.example.smartpassuserdevice.viewmodel.MyCardsViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

@Composable
fun ScreenCards(
    navController: NavHostController,
    cardsRepository: CardsRepository,
    viewModelStoreOwner: ViewModelStoreOwner
) {
    val context = LocalContext.current
    val factory = remember { MyCardsViewModel.MyCardsViewModelFactory(cardsRepository) }
    val myCardsViewModel: MyCardsViewModel = viewModel(viewModelStoreOwner = viewModelStoreOwner, factory = factory)

    // Используем collectAsState для получения данных из StateFlow
    val cardsState = myCardsViewModel.cardsState.collectAsState().value

    Column {
        // Заголовок экрана
        Text("Cards Screen")

        // Кнопка для получения карт
        Button(onClick = {
            handleGetMyCards(myCardsViewModel, navController, context) {
                Log.e("ScreenCards", "Error fetching cards")
            }
        }) {
            Text("Fetch My Cards")
        }

        Spacer(modifier = Modifier.height(16.dp))

        // Отображаем карусель с картами, если они есть
        if (cardsState.isNotEmpty()) {
            AccessCardCarousel(accessCards = cardsState, onUseAsMain = { selectedCard ->
                // Логика использования карты как основной
                Log.d("ScreenCards", "Using ${selectedCard.cardType} as main card")
            })
        } else {
            Text("No cards available")
        }
    }
}

@Composable
fun AccessCardCarousel(accessCards: List<AccessCard>, onUseAsMain: (AccessCard) -> Unit) {
    LazyRow(
        modifier = Modifier.fillMaxWidth(),
        horizontalArrangement = Arrangement.spacedBy(16.dp)
    ) {
        items(accessCards) { card ->
            CardItem(card = card, onUseAsMain = onUseAsMain)
        }
    }
}

@Composable
fun CardItem(card: AccessCard, onUseAsMain: (AccessCard) -> Unit) {
    Card(
        modifier = Modifier.padding(8.dp),
        elevation = CardDefaults.cardElevation(4.dp)
    ) {
        Column(
            modifier = Modifier.padding(16.dp),
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            // Отображение информации о карточке
            Text(text = "Card Type: ${card.cardType}")
            Text(text = "Description: ${card.description}")
            Text(text = "Access Level: ${card.accessLevelName}")

            Spacer(modifier = Modifier.height(16.dp))

            // Кнопка "Use as Main"
            Button(onClick = { onUseAsMain(card) }) {
                Text("Use as Main")
            }
        }
    }
}

private fun handleGetMyCards(
    myCardsViewModel: MyCardsViewModel,
    navController: NavHostController,
    context: Context,
    onError: () -> Unit
) {
    val coroutineScope: CoroutineScope = CoroutineScope(Dispatchers.IO)
    coroutineScope.launch {
        val result = myCardsViewModel.getMyCards("Bearer " + CacheUtils.getTokenFromCache(context).toString())

        withContext(Dispatchers.Main) {
            if (result.isSuccess) {
                val getMyCardsResponse = result.getOrNull()
                if (getMyCardsResponse != null) {
                    CacheUtils.saveUserCardsResponseToCache(context, getMyCardsResponse)
                    Log.d("CacheUtils", "Saved UserCards: ${CacheUtils.getUserCardsFromCache(context)}")

                    // Обновляем состояние ViewModel с карточками
                    myCardsViewModel.updateCardsState(getMyCardsResponse.accessCards)
                    navController.navigate("Cards") { popUpTo("Cards") { inclusive = true } }
                } else {
                    onError()
                }
            } else {
                onError()
            }
        }
    }
}
