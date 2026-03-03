import { handleCategorySubmit, loadCategories } from "./categories.js";
import { handleTransactionSubmit, loadTransactions } from "./transactions.js";
import {loadStats, setupStatsListeners} from "./stats.js";

export async function setupDashboard() {
    document.getElementById("category-form").addEventListener("submit", handleCategorySubmit);
    document.getElementById("transaction-form").addEventListener("submit", handleTransactionSubmit);

    document.getElementById("date").value = new Date().toISOString().substring(0, 10);

    setupStatsListeners();

    await loadCategories();
    await loadTransactions();
    await loadStats()
}