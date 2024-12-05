package com.example.smartpassuserdevice.ui.navigation

import android.util.Log
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.lifecycle.ViewModelStoreOwner
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import com.example.smartpassuserdevice.data.repository.CardsRepository
import com.example.smartpassuserdevice.ui.view.ScreenCards
import com.example.smartpassuserdevice.ui.view.ScreenUser
import android.content.Context

@Composable
fun NavigationGraph(
    navController: NavHostController,
    modifier: Modifier = Modifier,
    cardsRepository: CardsRepository,
    viewModelStoreOwner: ViewModelStoreOwner
) {
    NavHost(
        navController = navController,
        startDestination = "User",
        modifier = modifier
    ) {
        composable("User") {
            ScreenUser()
        }
        composable("Cards") {
            ScreenCards(cardsRepository, viewModelStoreOwner)
        }
    }
}


