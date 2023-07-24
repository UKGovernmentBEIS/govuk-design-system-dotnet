using System;
using System.Collections.Generic;
using GovUkDesignSystem.GovUkDesignSystemComponents;

namespace GovUkDesignSystem.Helpers;

// Helper to allow easy generation of pagination controls when no customisation is needed.
// Automatically shows and hides "Next" and "Previous" links and ellipsis as required.
public static class SimplePaginationHelper
{
    public static PaginationViewModel GetViewModel(
        int currentPage,
        string[] allPageUrls)
    {
        // If there is only one page (or no pages) of results don't show pagination controls
        if (allPageUrls.Length <= 1)
        {
            return null;
        }
        
        var maximumPage = allPageUrls.Length;
        
        if (currentPage < 1
            || currentPage > maximumPage)
        {
            throw new ArgumentOutOfRangeException();
        }

        var paginationLinks = new List<PaginationItemViewModel>
        {
            new ()
            {
                Number = "1",
                Current = currentPage == 1,
                Href = allPageUrls[0]
            }
        };

        for (var pageNumber = 2; pageNumber < maximumPage; pageNumber++)
        {
            if (pageNumber < currentPage - 2
                || pageNumber > currentPage + 2)
            {
                continue;
            }

            if ((pageNumber == currentPage - 2 && pageNumber != 2)
                || (pageNumber == currentPage + 2) && pageNumber != maximumPage - 1)
            {
                paginationLinks.Add(new PaginationItemViewModel()
                {
                    Ellipsis = true
                });
            }
            else if (pageNumber >= currentPage - 2
                && pageNumber <= currentPage + 2)
            {
                paginationLinks.Add(new PaginationItemViewModel()
                {
                    Number = pageNumber.ToString(),
                    Current = currentPage == pageNumber,
                    Href = allPageUrls[pageNumber - 1]
                });
            }
        }

        paginationLinks.Add(new PaginationItemViewModel()
        {
            Number = maximumPage.ToString(),
            Current = currentPage == maximumPage,
            Href = allPageUrls[maximumPage - 1]
        });
        
        var paginationDetails = new PaginationViewModel()
        {
            Items = paginationLinks
        };
        
        if (currentPage > 1)
        {
            paginationDetails.Previous = new PaginationLinkViewModel()
            {
                Href = allPageUrls[currentPage - 2]
            };
        }
    
        if (currentPage < maximumPage)
        {
            paginationDetails.Next = new PaginationLinkViewModel()
            {
                Href = allPageUrls[currentPage]
            };
        }

        return paginationDetails;
    }
}