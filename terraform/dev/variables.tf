variable "resource_group_name" {
  description = "Azure resource group"
  type        = string
  default     = "myTFResourceGroup"
}

variable "location" {
  description = "Azure region"
  type        = string
  default     = "westeurope"
}

variable "app_service_plan" {
  description = "Azure App Service Plan"
  type        = string
  default     = "P1v2"
}

variable "os_type" {
  description = "Azure OS Type"
  type        = string
  default     = "Windows"
}
