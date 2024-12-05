package com.example.smartpassuserdevice.ui.view

import android.content.Context
import android.util.Log
import android.widget.Toast
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.size
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.automirrored.filled.Logout
import androidx.compose.material.icons.filled.MoreVert
import androidx.compose.material.icons.filled.Refresh
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.ViewModelStoreOwner
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavHostController
import com.example.smartpassuserdevice.data.repository.UserRepository
import com.example.smartpassuserdevice.viewmodel.UserViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun TopNavigationBar(
    screenTitle: String,
    isMenuExpanded: Boolean,
    primaryNavController: NavHostController,
    secondaryNavController: NavHostController,
    viewModelStoreOwner: ViewModelStoreOwner,
    repository: UserRepository,
    onMenuToggle: () -> Unit,
    onMenuDismiss: () -> Unit
) {
    val context = LocalContext.current
    var isError by remember { mutableStateOf(false) }
    val factory = remember { UserViewModel.LoginViewModelFactory(repository) }
    val loginViewModel: UserViewModel = viewModel(viewModelStoreOwner = viewModelStoreOwner, factory = factory)

    Log.d("NavigationProblems", "Zashli v TopNavigationBar")
    TopAppBar(
        colors = TopAppBarDefaults.topAppBarColors(containerColor = MaterialTheme.colorScheme.primary),
        title = {
            Text(
                text = screenTitle,
                fontSize = 40.sp,
                color = MaterialTheme.colorScheme.onPrimary,
                fontWeight = FontWeight.Bold
            )
        },
        actions = {
            IconButton(onClick = onMenuToggle) {
                Box(
                    modifier = Modifier
                        .size(60.dp)
                        .background(
                            color = if (isMenuExpanded) MaterialTheme.colorScheme.onPrimary else Color.Transparent,
                            shape = CircleShape
                        ),
                    contentAlignment = Alignment.Center
                ) {
                    Icon(
                        modifier = Modifier.size(60.dp),
                        imageVector = Icons.Filled.MoreVert,
                        contentDescription = "More",
                        tint = if (isMenuExpanded) MaterialTheme.colorScheme.secondary else MaterialTheme.colorScheme.onPrimary // Цвет иконки при выделении
                    )
                }
            }
            DropdownMenu(
                expanded = isMenuExpanded,
                onDismissRequest = onMenuDismiss,
            ) {

                DropdownMenuItem(
                    onClick = {
                        Toast.makeText(context, "Auth Refresh", Toast.LENGTH_SHORT).show()
                        handleTokenRefresh(loginViewModel, primaryNavController, secondaryNavController, context, { isError = true })
                        onMenuDismiss()

                    },
                    text = {
                        Row {
                            Icon(
                                imageVector = Icons.Filled.Refresh,
                                contentDescription = "Refresh",
                                modifier = Modifier.size(30.dp),
                                tint = MaterialTheme.colorScheme.primary
                            )
                            Text(
                                text = "Auth Refresh",
                                fontSize = 30.sp,
                                modifier = Modifier.padding(start = 8.dp),
                                color = MaterialTheme.colorScheme.primary
                            )
                        }
                    }
                )

                DropdownMenuItem(
                    onClick = {
                        Toast.makeText(context, "Logout", Toast.LENGTH_SHORT).show()
                        handleLogout(navController = primaryNavController, secondaryNavController, context = context)
                        onMenuDismiss()
                    },
                    text = {
                        Row {
                            Icon(
                                imageVector = Icons.AutoMirrored.Filled.Logout,
                                contentDescription = "Logout",
                                modifier = Modifier.size(30.dp),
                                tint = MaterialTheme.colorScheme.primary
                            )
                            Text(
                                text = "Logout",
                                fontSize = 30.sp,
                                modifier = Modifier.padding(start = 8.dp),
                                color = MaterialTheme.colorScheme.primary
                            )
                        }
                    }
                )
            }
        }
    )
}

private fun handleLogout(
    navController: NavHostController,
    secondaryNavController: NavHostController,
    context: Context
) {
    CacheUtils.clearCache(context)
    secondaryNavController.popBackStack()
    Toast.makeText(context, "You have logged out", Toast.LENGTH_SHORT).show()
    navController.navigate("Login") {
        popUpTo("NavigationBar") { inclusive = true }
    }
}

private fun handleTokenRefresh(
    loginViewModel: UserViewModel,
    primaryNavController: NavHostController,
    secondaryNavController: NavHostController,
    context: Context,
    onError: () -> Unit
) {
    val coroutineScope: CoroutineScope = CoroutineScope(Dispatchers.IO)
    coroutineScope.launch {

        val token  = CacheUtils.getRefreshTokenDataFromCache(context)
        if (token == null) {
            primaryNavController.navigate("NavigationBar") { popUpTo("Login") { inclusive = true } }
        }

        val result = loginViewModel.refreshToken(token.toString())

        withContext(Dispatchers.Main) {
            if (result.isSuccess) {
                val responseBody = result.getOrNull()
                if (responseBody != null) {
                    // Если токен успешно обновлен, сохраняем новый ответ в кэш
                    CacheUtils.saveLoginResponseToCache(context, responseBody)
                    Log.d("CacheUtils", "Saved Token: ${CacheUtils.getTokenFromCache(context)}")
                    Log.d("CacheUtils", "Saved Refresh Token: ${CacheUtils.getRefreshTokenDataFromCache(context)}")
                    Log.d("CacheUtils", "Saved User Data: ${CacheUtils.getUserInfoFromCache(context)}")

                    secondaryNavController.popBackStack()
                    // Навигация на другую страницу
                    primaryNavController.navigate("NavigationBar") {
                        //popUpTo("User") { inclusive = true }
                    }
                } else {
                    // Если ответ пустой (ошибка обновления токена), вызываем onError
                    onError()
                }
            } else {
                // В случае неудачи вызываем onError
                onError()
            }
        }
    }
}
