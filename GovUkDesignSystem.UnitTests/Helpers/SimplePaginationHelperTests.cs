using System.Linq;
using FluentAssertions;
using GovUkDesignSystem.Helpers;
using Xunit;

namespace GovUkDesignSystem.UnitTests.Helpers
{
    public class SimplePaginationHelperTests
    {
        [Theory]
        [InlineData(1, 2, false, true, "1", "2")]
        [InlineData(2, 2, true, false, "1", "2")]
        [InlineData(1, 3, false, true, "1", "2", "3")]
        [InlineData(2, 3, true, true, "1", "2", "3")]
        [InlineData(3, 3, true, false, "1", "2", "3")]
        [InlineData(1, 4, false, true, "1", "2", "3", "4")]
        [InlineData(2, 4, true, true, "1", "2", "3", "4")]
        [InlineData(3, 4, true, true, "1", "2", "3", "4")]
        [InlineData(4, 4, true, false, "1", "2", "3", "4")]
        [InlineData(1, 5, false, true, "1", "2", "...", "5")]
        [InlineData(2, 5, true, true, "1", "2", "3", "4", "5")]
        [InlineData(3, 5, true, true, "1", "2", "3", "4", "5")]
        [InlineData(4, 5, true, true, "1", "2", "3", "4", "5")]
        [InlineData(5, 5, true, false, "1", "...", "4", "5")]
        [InlineData(1, 6, false, true, "1", "2", "...", "6")]
        [InlineData(2, 6, true, true, "1", "2", "3", "...", "6")]
        [InlineData(3, 6, true, true, "1", "2", "3", "4", "5", "6")]
        [InlineData(4, 6, true, true, "1", "2", "3", "4", "5", "6")]
        [InlineData(5, 6, true, true, "1", "...", "4", "5", "6")]
        [InlineData(6, 6, true, false, "1", "...", "5", "6")]
        [InlineData(1, 7, false, true, "1", "2", "...", "7")]
        [InlineData(2, 7, true, true, "1", "2", "3", "...", "7")]
        [InlineData(3, 7, true, true, "1", "2", "3", "4", "...", "7")]
        [InlineData(4, 7, true, true, "1", "2", "3", "4", "5", "6", "7")]
        [InlineData(5, 7, true, true, "1", "...", "4", "5", "6", "7")]
        [InlineData(6, 7, true, true, "1", "...", "5", "6", "7")]
        [InlineData(7, 7, true, false, "1", "...", "6", "7")]
        [InlineData(5, 9, true, true, "1", "...", "4", "5", "6", "...", "9")]
        public void GetViewModel_WithPageNumbers_GeneratesCorrectPaginationItems(
            int currentPage,
            int maximumPage,
            bool shouldShowPreviousLink,
            bool shouldShowNextLink,
            params string[] expectedLinkTexts)
        {
            // Arrange
            var pageUrls = Enumerable.Range(1, maximumPage).Select(r => $"url_{r}").ToArray();
            
            // Act
            var result = SimplePaginationHelper.GetViewModel(currentPage, pageUrls);
            
            // Assert
            if (shouldShowPreviousLink)
            {
                result.Previous.Should().NotBeNull();
                result.Previous.Href.Should().Be(pageUrls[currentPage - 2]);
            }
            else
            {
                result.Previous.Should().BeNull();
            }
            if (shouldShowNextLink)
            {
                result.Next.Should().NotBeNull();
                result.Next.Href.Should().Be(pageUrls[currentPage]);
            }
            else
            {
                result.Next.Should().BeNull();
            }

            var links = result.Items;
            links.Count.Should().Be(expectedLinkTexts.Length);

            for (var i = 0; i < links.Count; i++)
            {
                if (expectedLinkTexts[i] == "...")
                {
                    links[i].Ellipsis.Should().BeTrue();
                }
                else
                {
                    links[i].Ellipsis.Should().BeFalse();
                    links[i].Number.Should().Be(expectedLinkTexts[i]);
                    links[i].Href.Should().Be($"url_{expectedLinkTexts[i]}");
                }
            }
        }
    }
}