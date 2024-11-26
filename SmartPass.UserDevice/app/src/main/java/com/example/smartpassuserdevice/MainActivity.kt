package com.example.smartpassuserdevice

import AppNavigation
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import com.example.smartpassuserdevice.data.repository.CardsRepository
import com.example.smartpassuserdevice.ui.apiFunctionality.ApiServiceFactory
import com.example.smartpassuserdevice.data.repository.UserRepository
import com.example.smartpassuserdevice.ui.theme.SmartPassUserDeviceTheme

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
            SmartPassUserDeviceTheme {
                AppNavigation(
                    this,
                    UserRepository(ApiServiceFactory()),
                    CardsRepository(ApiServiceFactory())
                )
            }
        }
    }
}
