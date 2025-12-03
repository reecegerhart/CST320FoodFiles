"""
Authors: Emma Rogoveanu and Emma Schaffner
Course: CST 305
Assignment: Benchmark - Project 7 - Code Errors and Butterfly Effect
Packages: numpy and matplotlib
Description: M/M/1 scaling metrics plot 
"""
import numpy as np
import matplotlib.pyplot as plt

lam = 2     # base arrival rate
mu = 5      # base service rate

# Range of k values (scaling factor)
k_values = np.linspace(0.1, 10, 200)

rho_values = (k_values * lam) / (k_values * mu)     # utilization stays constant
X_values   = k_values * lam                         # throughput increases linearly
EN_values  = rho_values / (1 - rho_values)          # mean number in system
ET_values  = 1 / (k_values * (mu - lam))            # mean time in system decreases as 1/k

# One figure with four subplots
fig, axes = plt.subplots(2, 2, figsize=(12, 10))
fig.suptitle("Effect of Scaling λ and μ by Factor k in an M/M/1 System", fontsize=16)

# --- Subplot 1: Utilization ρ ---
axes[0, 0].plot(k_values, rho_values, linewidth=2)
axes[0, 0].set_title("Utilization ρ vs k")
axes[0, 0].set_xlabel("k")
axes[0, 0].set_ylabel("ρ")
axes[0, 0].grid(True)

# --- Subplot 2: Throughput X ---
axes[0, 1].plot(k_values, X_values, linewidth=2)
axes[0, 1].set_title("Throughput X vs k")
axes[0, 1].set_xlabel("k")
axes[0, 1].set_ylabel("Throughput X (jobs/sec)")
axes[0, 1].grid(True)

# --- Subplot 3: Mean Number in System E[N] ---
axes[1, 0].plot(k_values, EN_values, linewidth=2)
axes[1, 0].set_title("Mean Number in System E[N] vs k")
axes[1, 0].set_xlabel("k")
axes[1, 0].set_ylabel("E[N]")
axes[1, 0].grid(True)

# --- Subplot 4: Mean Time in System E[T] ---
axes[1, 1].plot(k_values, ET_values, linewidth=2)
axes[1, 1].set_title("Mean Time in System E[T] vs k")
axes[1, 1].set_xlabel("k")
axes[1, 1].set_ylabel("E[T] (seconds)")
axes[1, 1].grid(True)

plt.tight_layout(rect=[0, 0.03, 1, 0.95])
plt.show()
