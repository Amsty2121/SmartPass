package com.example.smartpassuserdevice.ui.view

import android.annotation.SuppressLint
import android.content.Context
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableIntStateOf
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.lifecycle.ViewModelStoreOwner
import androidx.navigation.NavGraph.Companion.findStartDestination
import androidx.navigation.NavHostController
import androidx.navigation.compose.rememberNavController
import com.example.smartpassuserdevice.data.repository.CardsRepository
import com.example.smartpassuserdevice.data.repository.UserRepository
import com.example.smartpassuserdevice.ui.navigation.NavigationGraph

@SuppressLint("UnusedMaterial3ScaffoldPaddingParameter")
@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun NavigationBarScreen(
    navController: NavHostController,
    viewModelStoreOwner: ViewModelStoreOwner,
    userRepository: UserRepository,
    cardsRepository: CardsRepository,
) {
    val secondaryNavController = rememberNavController()
    var selectedItemIndex by rememberSaveable { mutableIntStateOf(0) }
    val isMenuExpanded = rememberSaveable { mutableStateOf(false) }

    Scaffold(
        topBar = {
            TopNavigationBar(
                screenTitle = when (selectedItemIndex) {
                    0 -> "User"
                    1 -> "Cards"
                    else -> "User"
                },
                isMenuExpanded = isMenuExpanded.value,
                onMenuToggle = { isMenuExpanded.value = !isMenuExpanded.value },
                onMenuDismiss = { isMenuExpanded.value = false },
                primaryNavController = navController,
                secondaryNavController = secondaryNavController,
                viewModelStoreOwner = viewModelStoreOwner,
                repository = userRepository
            )
        },
        bottomBar = {
            BottomNavigationBar(
                selectedItemIndex = selectedItemIndex,
                onItemSelected = { index ->
                    val route = when (index) {
                        0 -> "User"
                        1 -> "Cards"
                        else -> "User"
                    }
                    navigateIfNeeded(secondaryNavController, route)
                    selectedItemIndex = index
                }
            )
        }
    ) { padding ->
        NavigationGraph(
            navController = secondaryNavController,
            modifier = Modifier.padding(padding),
            cardsRepository = cardsRepository,
            viewModelStoreOwner = viewModelStoreOwner,
        )
    }
}

private fun navigateIfNeeded(navController: NavHostController, targetRoute: String) {
    val currentRoute = navController.currentBackStackEntry?.destination?.route
    if (currentRoute != targetRoute) {
        navController.navigate(targetRoute) {
            popUpTo(navController.graph.findStartDestination().id) {
                saveState = true
            }
            launchSingleTop = true
            restoreState = true
        }
    }
}
