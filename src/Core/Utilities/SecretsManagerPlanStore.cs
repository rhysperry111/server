﻿using Bit.Core.Enums;
using Bit.Core.Models.StaticStore;

namespace Bit.Core.Utilities;

public static class SecretsManagerPlanStore
{
    public static IEnumerable<Plan> CreatePlan()
    {
        return new List<Plan>
        {
            new Plan
            {
                Type = PlanType.EnterpriseMonthly,
                Product = ProductType.Enterprise,
                BitwardenProduct = BitwardenProductType.SecretsManager,
                Name = "Enterprise (Monthly)",
                NameLocalizationKey = "planNameEnterprise",
                DescriptionLocalizationKey = "planDescEnterprise",
                CanBeUsedByBusiness = true,
                BaseSeats = 0,
                BaseServiceAccount = 200,
                HasAdditionalSeatsOption = true,
                HasAdditionalServiceAccountOption = true,
                TrialPeriodDays = 7,
                HasPolicies = true,
                HasGroups = true,
                HasDirectory = true,
                HasEvents = true,
                HasTotp = true,
                Has2fa = true,
                HasApi = true,
                HasSelfHost = true,
                HasSso = true,
                HasKeyConnector = true,
                HasScim = true,
                HasResetPassword = true,
                UsersGetPremium = true,
                HasCustomPermissions = true,
                UpgradeSortOrder = 3,
                DisplaySortOrder = 3,
                StripeSeatPlanId = "sm-enterprise-seat-monthly",
                StripeStoragePlanId = "service-account-monthly",
                BasePrice = 0,
                SeatPrice = 12,
                AdditionalPricePerServiceAccount = 0.5M,
                AllowSeatAutoscale = true,
            },
            new Plan
            {
                Type = PlanType.EnterpriseAnnually,
                Name = "Enterprise (Annually)",
                Product = ProductType.Enterprise,
                BitwardenProduct = BitwardenProductType.SecretsManager,
                IsAnnual = true,
                NameLocalizationKey = "planNameEnterprise",
                DescriptionLocalizationKey = "planDescEnterprise",
                CanBeUsedByBusiness = true,
                BaseSeats = 0,
                BaseServiceAccount = 200,
                HasAdditionalSeatsOption = true,
                HasAdditionalServiceAccountOption = true,
                TrialPeriodDays = 7,
                HasPolicies = true,
                HasSelfHost = true,
                HasGroups = true,
                HasDirectory = true,
                HasEvents = true,
                HasTotp = true,
                Has2fa = true,
                HasApi = true,
                HasSso = true,
                HasKeyConnector = true,
                HasScim = true,
                HasResetPassword = true,
                UsersGetPremium = true,
                HasCustomPermissions = true,
                UpgradeSortOrder = 3,
                DisplaySortOrder = 3,
                StripeSeatPlanId = "sm-enterprise-seat-annually",
                StripeStoragePlanId = "service-account-annually",
                BasePrice = 0,
                SeatPrice = 120,
                AdditionalPricePerServiceAccount = 0.5M,
                AllowSeatAutoscale = true,
            },
            new Plan
            {
                Type = PlanType.TeamsMonthly,
                Name = "Teams (Monthly)",
                Product = ProductType.Teams,
                BitwardenProduct = BitwardenProductType.SecretsManager,
                NameLocalizationKey = "planNameTeams",
                DescriptionLocalizationKey = "planDescTeams",
                CanBeUsedByBusiness = true,
                BaseSeats = 0,
                BaseServiceAccount = 50,
                HasAdditionalSeatsOption = true,
                HasAdditionalServiceAccountOption = true,
                TrialPeriodDays = 7,
                HasPolicies = true,
                HasSelfHost = true,
                HasGroups = true,
                HasDirectory = true,
                HasEvents = true,
                HasTotp = true,
                Has2fa = true,
                HasApi = true,
                HasSso = true,
                HasKeyConnector = true,
                HasScim = true,
                HasResetPassword = true,
                UsersGetPremium = true,
                HasCustomPermissions = true,
                UpgradeSortOrder = 3,
                DisplaySortOrder = 3,
                StripeSeatPlanId = "sm-teams-seat-monthly",
                StripeStoragePlanId = "service-account-monthly",
                BasePrice = 0,
                SeatPrice = 6,
                AdditionalPricePerServiceAccount = 0.5M,
                AllowSeatAutoscale = true,
            },
            new Plan
            {
                Type = PlanType.TeamsAnnually,
                Name = "Teams (Annually)",
                Product = ProductType.Teams,
                BitwardenProduct = BitwardenProductType.SecretsManager,
                IsAnnual = true,
                NameLocalizationKey = "planNameTeams",
                DescriptionLocalizationKey = "planDescTeams",
                CanBeUsedByBusiness = true,
                BaseSeats = 0,
                BaseServiceAccount = 50,
                HasAdditionalSeatsOption = true,
                HasAdditionalServiceAccountOption = true,
                TrialPeriodDays = 7,
                HasPolicies = true,
                HasSelfHost = true,
                HasGroups = true,
                HasDirectory = true,
                HasEvents = true,
                HasTotp = true,
                Has2fa = true,
                HasApi = true,
                HasSso = true,
                HasKeyConnector = true,
                HasScim = true,
                HasResetPassword = true,
                UsersGetPremium = true,
                HasCustomPermissions = true,
                UpgradeSortOrder = 3,
                DisplaySortOrder = 3,
                StripeSeatPlanId = "sm-teams-seat-annually",
                StripeStoragePlanId = "service-account-annually",
                BasePrice = 0,
                SeatPrice = 48,
                AdditionalPricePerServiceAccount = 0.5M,
                AllowSeatAutoscale = true,
            },
            new Plan
            {
                Type = PlanType.Free,
                Product = ProductType.Free,
                BitwardenProduct = BitwardenProductType.SecretsManager,
                Name = "Free",
                NameLocalizationKey = "planNameFree",
                DescriptionLocalizationKey = "planDescFree",
                BaseSeats = 2,
                BaseServiceAccount = 3,
                MaxProjects = 3,
                MaxUsers = 2,
                MaxServiceAccount = 3,
                UpgradeSortOrder = -1, // Always the lowest plan, cannot be upgraded to
                DisplaySortOrder = -1,
                AllowSeatAutoscale = false,
            }
        };
    }
}
