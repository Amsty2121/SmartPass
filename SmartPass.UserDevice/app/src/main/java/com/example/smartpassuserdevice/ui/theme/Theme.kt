package com.example.smartpassuserdevice.ui.theme

import android.app.Activity
import android.os.Build
import androidx.compose.foundation.isSystemInDarkTheme
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.darkColorScheme
import androidx.compose.material3.dynamicDarkColorScheme
import androidx.compose.material3.dynamicLightColorScheme
import androidx.compose.material3.lightColorScheme
import androidx.compose.runtime.Composable
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalContext

private val DarkColorScheme = darkColorScheme(
    primary = Color(0xFFFF9800),         // Основной цвет (оранжевый)
    onPrimary = Color(0xFF32353E),       // Цвет текста на основном цвете
    secondary = Color(0xFFFFC107),       // Более желтый оттенок для secondary
    tertiary = Color(0xFF3C3E47),
    onTertiary = Color(0xFFFFC107),// Дополнительный цвет для выделений
    background = Color(0xFF1C1B1F),      // Темный фон
    onBackground = Color(0xFFFF9800),    // Оранжевый для текста на фоне
    error = Color(0xFFD32F2F)            // Красный цвет для ошибок
)


private val LightColorScheme = lightColorScheme(
    primary = Color(0xFFFF9800), // #ff9800
    secondary = Color(0xFF32353E), // #32353e
    tertiary = Color(0xFF3C3E47)  // #3c3e47

    /* Other default colors to override
    background = Color(0xFFFFFBFE),
    surface = Color(0xFFFFFBFE),
    onPrimary = Color.White,
    onSecondary = Color.White,
    onTertiary = Color.White,
    onBackground = Color(0xFF1C1B1F),
    onSurface = Color(0xFF1C1B1F),
    */
)

@Composable
fun SmartPassUserDeviceTheme(
    darkTheme: Boolean = isSystemInDarkTheme(),
    // Dynamic color is available on Android 12+
    dynamicColor: Boolean = true,
    content: @Composable () -> Unit
) {
    /*val colorScheme = when {
      dynamicColor && Build.VERSION.SDK_INT >= Build.VERSION_CODES.S -> {
        val context = LocalContext.current
        if (darkTheme) dynamicDarkColorScheme(context) else dynamicLightColorScheme(context)
      }
      darkTheme -> DarkColorScheme
      else -> LightColorScheme
    }*/

    MaterialTheme(
      colorScheme = DarkColorScheme,
      typography = Typography,
      content = content
    )
}