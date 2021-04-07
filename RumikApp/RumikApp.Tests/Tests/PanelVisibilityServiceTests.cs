﻿using RumikApp.Services;
using System.Windows;
using Xunit;

namespace RumikApp.Tests
{
    public class PanelVisibilityServiceTests
    {
        PanelVisibilityService sut;

        #region setup
        public PanelVisibilityServiceTests()
        {
            sut = new PanelVisibilityService();
        }

        #endregion

        [Fact]
        void PanelVisibilityService_Is_Constructed()
        {
            // Arrange
            PanelVisibilityService actualResult;

            // Act
            actualResult = new PanelVisibilityService();

            // Assert
            Assert.True(actualResult.MainPanelVisibility == Visibility.Visible);
            Assert.True(actualResult.PollVisibility == Visibility.Collapsed);
            Assert.True(actualResult.InsertDataToDatabaseFormVisibility == Visibility.Collapsed);
            Assert.True(actualResult.DataGridViewModelVisibility == Visibility.Collapsed);
            Assert.True(actualResult.DataGridViewModel2Visibility == Visibility.Collapsed);
            Assert.True(actualResult.ItemsControlVisibility == Visibility.Collapsed);

        }

        [Fact]
        void MainPanelVisibility_Goes_Visible()
        {
            // Arrange

            // Act
            sut.MainPanelVisibility = Visibility.Visible;

            // Assert
            Assert.True(sut.MainPanelVisibility == Visibility.Visible);
            Assert.True(sut.PollVisibility == Visibility.Collapsed);
            Assert.True(sut.InsertDataToDatabaseFormVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModelVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModel2Visibility == Visibility.Collapsed);
            Assert.True(sut.ItemsControlVisibility == Visibility.Collapsed);
        }

        [Fact]
        void PollVisibility_Goes_Visible()
        {
            // Arrange

            // Act
            sut.PollVisibility = Visibility.Visible;

            // Assert
            Assert.True(sut.MainPanelVisibility == Visibility.Collapsed);
            Assert.True(sut.PollVisibility == Visibility.Visible);
            Assert.True(sut.InsertDataToDatabaseFormVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModelVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModel2Visibility == Visibility.Collapsed);
            Assert.True(sut.ItemsControlVisibility == Visibility.Collapsed);
        }

        [Fact]
        void InsertDataToDatabaseFormVisibility_Goes_Visible()
        {
            // Arrange

            // Act
            sut.InsertDataToDatabaseFormVisibility = Visibility.Visible;

            // Assert
            Assert.True(sut.MainPanelVisibility == Visibility.Collapsed);
            Assert.True(sut.PollVisibility == Visibility.Collapsed);
            Assert.True(sut.InsertDataToDatabaseFormVisibility == Visibility.Visible);
            Assert.True(sut.DataGridViewModelVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModel2Visibility == Visibility.Collapsed);
            Assert.True(sut.ItemsControlVisibility == Visibility.Collapsed);
        }

        [Fact]
        void DataGridViewModelVisibility_Goes_Visible()
        {
            // Arrange

            // Act
            sut.DataGridViewModelVisibility = Visibility.Visible;

            // Assert
            Assert.True(sut.MainPanelVisibility == Visibility.Collapsed);
            Assert.True(sut.PollVisibility == Visibility.Collapsed);
            Assert.True(sut.InsertDataToDatabaseFormVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModelVisibility == Visibility.Visible);
            Assert.True(sut.DataGridViewModel2Visibility == Visibility.Collapsed);
            Assert.True(sut.ItemsControlVisibility == Visibility.Collapsed);
        }

        [Fact]
        void DataGridViewModel2Visibility_Goes_Visible()
        {
            // Arrange

            // Act
            sut.DataGridViewModel2Visibility = Visibility.Visible;

            // Assert
            Assert.True(sut.MainPanelVisibility == Visibility.Collapsed);
            Assert.True(sut.PollVisibility == Visibility.Collapsed);
            Assert.True(sut.InsertDataToDatabaseFormVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModelVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModel2Visibility == Visibility.Visible);
            Assert.True(sut.ItemsControlVisibility == Visibility.Collapsed);
        }

        [Fact]
        void ItemsControlVisibility_Goes_Visible()
        {
            // Arrange

            // Act
            sut.ItemsControlVisibility = Visibility.Visible;

            // Assert
            Assert.True(sut.MainPanelVisibility == Visibility.Collapsed);
            Assert.True(sut.PollVisibility == Visibility.Collapsed);
            Assert.True(sut.InsertDataToDatabaseFormVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModelVisibility == Visibility.Collapsed);
            Assert.True(sut.DataGridViewModel2Visibility == Visibility.Collapsed);
            Assert.True(sut.ItemsControlVisibility == Visibility.Visible);
        }
    }
}