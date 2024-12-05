package com.example.smartpassuserdevice.ui.view

import android.util.Log
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Star
import androidx.compose.material3.Icon
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.MaterialTheme
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp

import androidx.compose.material3.NavigationBarItemDefaults
import androidx.compose.ui.graphics.vector.ImageVector

data class NavigationItem(
    val name: String,
    val logo: ImageVector
)

@Composable
fun BottomNavigationBar(
    selectedItemIndex: Int,
    onItemSelected: (Int) -> Unit
) {
    Log.d("NavigationProblems", "Zashli v BottomNavigationBar")

    val navItems = listOf(
        NavigationItem("User", Icons.Filled.AccountCircle),
        NavigationItem("Cards", Icons.Filled.Star)
    )

    NavigationBar(
        containerColor = MaterialTheme.colorScheme.primary,
        modifier = Modifier.padding(top = 2.dp, bottom = 0.dp)
    ) {
        navItems.forEachIndexed { index, item ->
            NavigationBarItem(
                selected = selectedItemIndex == index,
                onClick = { onItemSelected(index) },
                icon = {
                    Icon(
                        imageVector = item.logo,
                        contentDescription = item.name,
                        modifier = Modifier.size(45.dp)
                    )
                },
                colors = NavigationBarItemDefaults.colors(
                    selectedIconColor = MaterialTheme.colorScheme.secondary,  // Цвет иконки для выделенного элемента
                    unselectedIconColor = MaterialTheme.colorScheme.onPrimary, // Цвет иконки для невыделенного элемента
                    indicatorColor = MaterialTheme.colorScheme.onPrimary         // Цвет фона для выделенного элемента
                )
            )
        }
    }
}