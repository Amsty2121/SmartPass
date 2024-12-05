package com.example.smartpassuserdevice.ui.view

import CacheUtils
import android.content.Context
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.ViewModelStoreOwner
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavHostController
import com.example.smartpassuserdevice.data.model.LoginRequest
import com.example.smartpassuserdevice.data.repository.UserRepository
import com.example.smartpassuserdevice.viewmodel.UserViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Visibility
import androidx.compose.material.icons.filled.VisibilityOff
import androidx.compose.ui.text.input.VisualTransformation
import android.util.Log
import androidx.compose.foundation.clickable
import androidx.compose.ui.text.TextStyle

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun LoginScreen(
    navController: NavHostController,
    viewModelStoreOwner: ViewModelStoreOwner,
    userRepository: UserRepository
) {
    val context = LocalContext.current

    if (CacheUtils.getTokenFromCache(context) != null ) {
        LaunchedEffect(Unit) {
            navController.navigate("NavigationBar") {
                popUpTo("Login") { inclusive = true }
            }
        }
        return
    }

    var username by remember { mutableStateOf("") }
    var password by remember { mutableStateOf("") }
    var isError by remember { mutableStateOf(false) }

    val factory = remember { UserViewModel.LoginViewModelFactory(userRepository) }
    val loginViewModel: UserViewModel = viewModel(viewModelStoreOwner = viewModelStoreOwner, factory = factory)


    Column(
        modifier = Modifier
            .fillMaxSize()
            .background(MaterialTheme.colorScheme.background)
            .padding(16.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        TitleText()
        Spacer(modifier = Modifier.height(16.dp))

        UsernameField(username, onUsernameChange = { username = it }, isError = isError)
        Spacer(modifier = Modifier.height(8.dp))

        PasswordField(password, onPasswordChange = { password = it }, isError = isError)
        Spacer(modifier = Modifier.height(16.dp))

        LoginButton(
            onClick = { handleLogin(loginViewModel, navController, context, username, password, { isError = true }) }
        )

        if (isError) {
            Spacer(modifier = Modifier.height(8.dp))
            ErrorMessage()
        }
    }
}

@Composable
private fun TitleText() {
    Text(
        text = "Enter Credentials",
        fontSize = 24.sp,
        fontWeight = FontWeight.Bold,
        color = MaterialTheme.colorScheme.primary
    )
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
private fun UsernameField(username: String, onUsernameChange: (String) -> Unit, isError: Boolean) {
    OutlinedTextField(
        value = username,
        onValueChange = {
            if (it.length <= 20 && it.all { char -> isValidLoginCharacter(char) }) {
                onUsernameChange(it)
            }
        },
        label = {
            Text(
                text = "Username",
                fontSize = 20.sp,
                fontWeight = FontWeight.Medium
            )
        },
        isError = isError && username.isEmpty(),
        textStyle = TextStyle(
            fontSize = 20.sp,
            color = MaterialTheme.colorScheme.onBackground
        ),
        colors = TextFieldDefaults.outlinedTextFieldColors(
            focusedTextColor = MaterialTheme.colorScheme.onBackground,
            unfocusedTextColor = MaterialTheme.colorScheme.secondary,
            containerColor = MaterialTheme.colorScheme.background,
            focusedLabelColor = MaterialTheme.colorScheme.secondary,
            unfocusedLabelColor = MaterialTheme.colorScheme.onBackground,
            cursorColor = MaterialTheme.colorScheme.primary,
            focusedBorderColor = MaterialTheme.colorScheme.secondary,
            unfocusedBorderColor = MaterialTheme.colorScheme.onBackground,
            errorBorderColor = MaterialTheme.colorScheme.error,
            errorLabelColor = MaterialTheme.colorScheme.error
        )
    )
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
private fun PasswordField(password: String, onPasswordChange: (String) -> Unit, isError: Boolean) {
    var passwordVisible by remember { mutableStateOf(false) }

    OutlinedTextField(
        value = password,
        onValueChange = {
            if (it.length <= 20 && it.all { char -> isValidPasswordCharacter(char) }) {
                onPasswordChange(it)
            }
        },
        label = {
            Text(
                text = "Password",
                fontSize = 20.sp,
                fontWeight = FontWeight.Medium
            )
        },
        isError = isError && password.isEmpty(),
        visualTransformation = if (passwordVisible) VisualTransformation.None else PasswordVisualTransformation(),
        textStyle = TextStyle(fontSize = 20.sp),
        trailingIcon = {
            PasswordVisibilityIcon(passwordVisible) { passwordVisible = !passwordVisible }
        },
        colors = TextFieldDefaults.outlinedTextFieldColors(
            focusedTextColor = MaterialTheme.colorScheme.onBackground,
            unfocusedTextColor = MaterialTheme.colorScheme.secondary,
            containerColor = MaterialTheme.colorScheme.background,
            focusedLabelColor = MaterialTheme.colorScheme.secondary,
            unfocusedLabelColor = MaterialTheme.colorScheme.onBackground,
            cursorColor = MaterialTheme.colorScheme.primary,
            focusedBorderColor = MaterialTheme.colorScheme.secondary,
            unfocusedBorderColor = MaterialTheme.colorScheme.onBackground,
            errorBorderColor = MaterialTheme.colorScheme.error,
            errorLabelColor = MaterialTheme.colorScheme.error
        )
    )
}

@Composable
private fun PasswordVisibilityIcon(passwordVisible: Boolean, onClick: () -> Unit) {
    Icon(
        imageVector = if (passwordVisible) Icons.Filled.Visibility else Icons.Filled.VisibilityOff,
        contentDescription = if (passwordVisible) "Hide password" else "Show password",
        modifier = Modifier
            .size(24.dp)
            .clickable { onClick() },
        tint = MaterialTheme.colorScheme.secondary
    )
}

@Composable
private fun LoginButton(onClick: () -> Unit) {
    Button(
        onClick = onClick,
        colors = ButtonDefaults.buttonColors(
            containerColor = MaterialTheme.colorScheme.primary,
            contentColor = MaterialTheme.colorScheme.onPrimary
        )
    ) {
        Text(
            text = "Login",
            fontSize = 24.sp,
            fontWeight = FontWeight.Black
        )
    }
}

@Composable
private fun ErrorMessage() {
    Text(
        "Invalid credentials, please try again.",
        color = MaterialTheme.colorScheme.error
    )
}

private fun isValidLoginCharacter(char: Char): Boolean {
    return char.isLetterOrDigit() || char in listOf('.', '_', '-')
}

private fun isValidPasswordCharacter(char: Char): Boolean {
    return char.isLetterOrDigit() || char in listOf('.', '_', '-', '!', '/')
}

private fun handleLogin(
    loginViewModel: UserViewModel,
    navController: NavHostController,
    context: Context,
    username: String,
    password: String,
    onError: () -> Unit
) {
    val coroutineScope: CoroutineScope = CoroutineScope(Dispatchers.IO)
    coroutineScope.launch {
        val result = loginViewModel.login(LoginRequest(username, password))

        withContext(Dispatchers.Main) {
            if (result.isSuccess) {
                val loginResponse = result.getOrNull()
                if (loginResponse != null) {
                    CacheUtils.saveLoginResponseToCache(context, loginResponse)
                    Log.d("CacheUtils", "Saved Token: ${CacheUtils.getTokenFromCache(context)}")
                    Log.d("CacheUtils", "Saved Refresh Token: ${CacheUtils.getRefreshTokenDataFromCache(context)}")
                    Log.d("CacheUtils", "Saved User Data: ${CacheUtils.getUserInfoFromCache(context)}")
                    //Log.d("CacheUtils", "Saved AuthKey: ${CacheUtils.getAuthKeyFromCache(context)}")
                    //CacheUtils.clearCache(context)
                    navController.navigate("NavigationBar") { popUpTo("Login") { inclusive = true } }
                } else {
                    onError()
                }
            } else {
                onError()
            }
        }
    }
}
