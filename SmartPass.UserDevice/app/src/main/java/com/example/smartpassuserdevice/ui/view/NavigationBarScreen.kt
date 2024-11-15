package com.example.smartpassuserdevice.ui.view

import android.annotation.SuppressLint
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
import com.example.smartpassuserdevice.data.repository.UserRepository
import com.example.smartpassuserdevice.ui.navigation.NavigationGraph

@SuppressLint("UnusedMaterial3ScaffoldPaddingParameter")
@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun NavigationBarScreen(
    navController: NavHostController,
    viewModelStoreOwner: ViewModelStoreOwner,
    repository: UserRepository
) {
    val secondaryNavController = rememberNavController()
    var selectedItemIndex by rememberSaveable { mutableIntStateOf(0) }
    var isMenuExpanded by rememberSaveable { mutableStateOf(false) }

    Scaffold(
        topBar = {
            TopNavigationBar(
                screenTitle = when (selectedItemIndex) {
                    0 -> "User"
                    1 -> "Cards"
                    else -> "User"
                },
                isMenuExpanded = isMenuExpanded,
                onMenuToggle = { isMenuExpanded = !isMenuExpanded },
                onMenuDismiss = { isMenuExpanded = false },
                primaryNavController = navController,
                secondaryNavController = secondaryNavController,
                viewModelStoreOwner = viewModelStoreOwner,
                repository = repository
            )
        },
        bottomBar = {
            BottomNavigationBar(
                selectedItemIndex = selectedItemIndex,
                onItemSelected = { index ->
                    selectedItemIndex = index
                    val route = when (index) {
                        0 -> "User"
                        1 -> "Cards"
                        else -> "User"
                    }
                    secondaryNavController.navigate(route) {
                        popUpTo(secondaryNavController.graph.findStartDestination().id) {
                            saveState = true
                        }
                        launchSingleTop = true
                        restoreState = true
                    }
                }
            )
        }
    ) { padding ->
        NavigationGraph(navController = secondaryNavController, modifier = Modifier.padding(padding))
    }
}
