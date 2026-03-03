import { apiCall } from "./httpClient.js";
import { setupAuthListeners } from "./auth.js";
import { setupDashboard } from "./dashboard.js";

document.addEventListener("DOMContentLoaded", async () => {
    const dashboardSection = document.getElementById("dashboard-section");

    setupAuthListeners();

    if (dashboardSection) {
        try {
            await apiCall("/auth/me");

            await setupDashboard();
        }
        catch (error) {
            console.error("Loading dashboard error: ", error);
        }
    }
})