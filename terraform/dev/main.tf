terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0.2"
    }
  }

  required_version = ">= 1.1.0"
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = var.resource_group_name
  location = var.location

  tags = {
    Environment = "dev"
    Team        = "engineering"
  }
}

resource "azurerm_service_plan" "asp" {
  name                = "gbail-gamin1challenge-asp"
  resource_group_name = azurerm_resource_group.rg.name
  location            = var.location
  sku_name            = var.app_service_plan
  os_type             = var.os_type
}

resource "azurerm_windows_web_app" "webapp" {
  name                = "gbail-gaming1challenge-dev"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_service_plan.asp.location
  service_plan_id     = azurerm_service_plan.asp.id

  site_config {}
}
