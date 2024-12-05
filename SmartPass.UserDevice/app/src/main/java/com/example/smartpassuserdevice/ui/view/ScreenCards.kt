package com.example.smartpassuserdevice.ui.view

import CacheUtils
import android.content.Context
import android.util.Log
import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.wrapContentSize
import androidx.compose.foundation.pager.PagerState
import androidx.compose.foundation.pager.VerticalPager
import androidx.compose.foundation.pager.rememberPagerState

import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.Button
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.ViewModelStoreOwner
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavHostController
import com.example.smartpassuserdevice.data.model.AccessCard
import com.example.smartpassuserdevice.data.repository.CardsRepository
import com.example.smartpassuserdevice.viewmodel.MyCardsViewModel
import com.google.accompanist.pager.ExperimentalPagerApi
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

@Composable
fun ScreenCards(
    cardsRepository: CardsRepository,
    viewModelStoreOwner: ViewModelStoreOwner
) {
    Log.d("NavigationProblems", "Zasli v cards")
    val context = LocalContext.current
    val factory = remember { MyCardsViewModel.MyCardsViewModelFactory(cardsRepository) }
    val myCardsViewModel: MyCardsViewModel =
        viewModel(viewModelStoreOwner = viewModelStoreOwner, factory = factory)



    // Запускаем проверку состояния кэша и API-запрос
    LaunchedEffect(Unit) {
        handleGetMyCardsFromCacheOrApi(myCardsViewModel, context)
    }

    // Состояние карточек
    val cardsState = myCardsViewModel.cardsState.collectAsState().value

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp),
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        // Вертикальный пейджер для отображения карточек
        if (cardsState.isNotEmpty()) {
            VerticalCardsPager(cards = cardsState, context = context)
        } else {
            Text(
                text = "No available cards",
                style = MaterialTheme.typography.bodyMedium.copy(
                    fontWeight = FontWeight.Bold,
                    fontSize = 24.sp // или любой другой размер
                ),
                modifier = Modifier
                    .fillMaxSize() // Заполняет весь экран
                    .wrapContentSize(Alignment.Center) // Центрирует текст
            )
        }
    }
}


@OptIn(ExperimentalPagerApi::class)
@Composable
fun VerticalCardsPager(cards: List<AccessCard>, context: Context) {
    Log.d("CacheUtils", "index in vertical cards pager: ${CacheUtils.getSelectedCardIndexFromCache(context)}")
    val pagerState = rememberPagerState(initialPage = CacheUtils.getSelectedCardIndexFromCache(context)) {
        cards.size
    }

    // Используем Box для ограничения высоты VerticalPager
    Box(
        modifier = Modifier
            .fillMaxSize(),
        contentAlignment = Alignment.Center // Центрируем пейджер внутри Box
    ) {
        // Перемещаем текст сюда, чтобы он был ближе к карусели
        Text(
            text = "Choose card",
            style = MaterialTheme.typography.headlineMedium.copy(fontWeight = FontWeight.Bold),
            modifier = Modifier
                .align(Alignment.TopCenter) // Размещаем текст вверху, ближе к карусели
                .padding(top = 30.dp) // Отступ от верхней границы
        )

        VerticalPager(
            state = pagerState,
            modifier = Modifier
                .fillMaxWidth()
                .height(400.dp), // Ограничиваем высоту пейджера
            contentPadding = PaddingValues(vertical = 50.dp)
        ) { index ->
            // Сохраняем индекс выбранной карты в кэш
            if (index == pagerState.currentPage) {
                Log.d("CacheUtils", "Saved Token: ${CacheUtils.getSelectedCardIndexFromCache(context)}")
                CacheUtils.saveSelectedCardIndexToCache(context, index)
                CacheUtils.saveSelectedCardToCache(context, cards[index])
            }
            CardContent(index = index, pagerState = pagerState, cards[index])
        }
    }
}






@OptIn(ExperimentalPagerApi::class)
@Composable
fun CardContent(index: Int, pagerState: PagerState, accessCard: AccessCard) {
    val isActive = index == pagerState.currentPage // Проверка, активна ли карточка

    Card(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp)
            .then(
                if (isActive) Modifier else Modifier.padding(horizontal = 16.dp)
            ) // Добавляем разные стили для активной карточки
            .border(
                width = 2.dp,
                color = MaterialTheme.colorScheme.primary, // Цвет бордера - primary
                shape = RoundedCornerShape(16.dp) // Закругление углов, чтобы соответствовать карточке
            ),
        elevation = CardDefaults.cardElevation(if (isActive) 8.dp else 4.dp),
        colors = CardDefaults.cardColors(
            containerColor = if (isActive) MaterialTheme.colorScheme.primary else MaterialTheme.colorScheme.surface
        ),
        shape = RoundedCornerShape(16.dp)
    ) {
        Box(
            modifier = Modifier.fillMaxSize()
        ) {
            // Основной контент карточки
            Column(
                modifier = Modifier
                    .padding(16.dp)
                    .align(Alignment.Center), // Центрируем остальные элементы в карточке
                verticalArrangement = Arrangement.spacedBy(8.dp),
                horizontalAlignment = Alignment.CenterHorizontally
            ) {
                if (isActive) {
                    Text(
                        text = "This card is in use",
                        style = MaterialTheme.typography.headlineSmall.copy(fontWeight = FontWeight.Bold),
                        color = MaterialTheme.colorScheme.onPrimary
                    )
                }
                Spacer(modifier = Modifier.height(8.dp))

                // Отображаем данные из AccessCard
                Text(
                    text = "Card Type: ${accessCard.cardType}",
                    style = MaterialTheme.typography.bodyMedium.copy(
                        fontSize = 18.sp // Устанавливаем нужный размер шрифта
                    ),
                    color = if (isActive) MaterialTheme.colorScheme.onPrimary else MaterialTheme.colorScheme.onSurface,
                )
                Text(
                    text = "Card State: ${accessCard.cardState}",
                    style = MaterialTheme.typography.bodyMedium.copy(
                        fontSize = 18.sp // Устанавливаем нужный размер шрифта
                    ),
                    color = if (isActive) MaterialTheme.colorScheme.onPrimary else MaterialTheme.colorScheme.onSurface,
                )
                Text(
                    text = "Access Level: ${accessCard.accessLevelName}",
                    style = MaterialTheme.typography.bodyMedium.copy(
                        fontSize = 18.sp // Устанавливаем нужный размер шрифта
                    ),
                    color = if (isActive) MaterialTheme.colorScheme.onPrimary else MaterialTheme.colorScheme.onSurface,
                )
                accessCard.description?.let {
                    Text(
                        text = "Description: $it",
                        style = MaterialTheme.typography.bodyMedium.copy(
                            fontSize = 18.sp // Устанавливаем нужный размер шрифта
                        ),
                        color = if (isActive) MaterialTheme.colorScheme.onPrimary else MaterialTheme.colorScheme.onSurface,
                    )
                }
                accessCard.lastUsingUtcDate?.let {
                    Text(
                        text = "Last Used: ${it.toString()}",
                        style = MaterialTheme.typography.bodyMedium.copy(
                            fontSize = 18.sp // Устанавливаем нужный размер шрифта
                        ),
                        color = if (isActive) MaterialTheme.colorScheme.onPrimary else MaterialTheme.colorScheme.onSurface,
                    )
                }
            }

            // Индекс карты в правом нижнем углу с изменяющимся цветом
            val indexToShow = index + 1
            Text(
                text = "Card: $indexToShow",
                style = if (isActive) MaterialTheme.typography.headlineSmall else MaterialTheme.typography.bodyMedium,
                color = if (isActive) MaterialTheme.colorScheme.onPrimary else MaterialTheme.colorScheme.onSurface,
                modifier = Modifier
                    .align(Alignment.BottomEnd) // Выравниваем по правому нижнему углу
                    .padding(16.dp) // Добавляем отступ для размещения от края
            )
        }
    }
}



private suspend fun handleGetMyCardsFromCacheOrApi(
    myCardsViewModel: MyCardsViewModel,
    context: Context
) {
    val cachedCardsResponse = CacheUtils.getUserCardsFromCache(context)

    if (cachedCardsResponse?.accessCards.isNullOrEmpty()) {
        // Если кэш пуст, делаем запрос к API
        Log.d("ScreenCards", "Cache is empty, fetching from API...")
        val result =
            myCardsViewModel.getMyCards("Bearer " + CacheUtils.getTokenFromCache(context).toString())

        if (result.isSuccess) {
            val getMyCardsResponse = result.getOrNull()
            if (getMyCardsResponse != null) {
                CacheUtils.saveUserCardsResponseToCache(context, getMyCardsResponse)
                Log.d("CacheUtils", "Saved UserCards to Cache")
                myCardsViewModel.updateCardsState(getMyCardsResponse.accessCards)
            } else {
                Log.e("ScreenCards", "Error fetching cards from API")
            }
        } else {
            Log.e("ScreenCards", "Failed to fetch cards from API")
        }
    } else {
        // Если кэш не пуст, обновляем состояние из кэша
        Log.d("ScreenCards", "Loaded cards from cache")
        Log.d("ScreenCards", cachedCardsResponse.toString())
        myCardsViewModel.updateCardsState(cachedCardsResponse!!.accessCards)
        Log.d("ScreenCards", myCardsViewModel.cardsState.value.toString())
    }
}
