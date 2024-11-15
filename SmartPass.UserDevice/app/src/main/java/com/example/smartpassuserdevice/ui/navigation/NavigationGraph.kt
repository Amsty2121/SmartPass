package com.example.smartpassuserdevice.ui.navigation

import android.util.Log
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import com.example.smartpassuserdevice.ui.view.ScreenCards
import com.example.smartpassuserdevice.ui.view.ScreenUser

@Composable
fun NavigationGraph(navController: NavHostController, modifier: Modifier = Modifier) {
    Log.d("NavigationProblems", "Zashli v NavHost")

    NavHost(
        navController = navController,
        startDestination = "User",
        modifier = modifier
    ) {
        composable("User") {
            Log.d("NavigationProblems", "Navigating to User screen")
            ScreenUser(navController)
        }
        composable("Cards") {
            Log.d("NavigationProblems", "Navigating to Cards screen")
            ScreenCards(navController)
        }
    }
}

