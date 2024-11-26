import androidx.compose.runtime.Composable
import androidx.lifecycle.ViewModelStoreOwner
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.example.smartpassuserdevice.data.repository.CardsRepository
import com.example.smartpassuserdevice.data.repository.UserRepository
import com.example.smartpassuserdevice.ui.view.LoginScreen
import com.example.smartpassuserdevice.ui.view.NavigationBarScreen

@Composable
fun AppNavigation(viewModelStoreOwner: ViewModelStoreOwner, userRepository: UserRepository, cardsRepository: CardsRepository) {
    val navController = rememberNavController()

    NavHost(navController = navController, startDestination = "Login") {
        composable("Login") { LoginScreen(navController, viewModelStoreOwner, userRepository) }
        composable("NavigationBar") { NavigationBarScreen(navController, viewModelStoreOwner, userRepository, cardsRepository) }
    }
}

