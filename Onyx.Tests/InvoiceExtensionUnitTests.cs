using FluentAssertions;
using Onyx.Application;
using Onyx.Application.Models;

namespace Onyx.Tests
{
    public class InvoiceExtensionUnitTests

    {
        [Fact]
        public void GetDuplicateGuestNamesAccrossGroups_ShouldReturnGuestNames()
        {
            var groups = GenerateTesInvoideGroups();

            var guestNames = InvoiceExtensions.GetRepeatedGuestNamesAccrossGroups(groups).ToList();

            guestNames.Count.Should().Be(1);
            guestNames.Should().BeEquivalentTo(["John"]);
        }

        [Fact]
        public void GetDuplicateGuestNamesAccrossGroups_ShouldNotReturnGuestNames()
        {
            var groups = GenerateUniqueTesInvoideGroups();

            var guestNames = InvoiceExtensions.GetRepeatedGuestNamesAccrossGroups(groups).ToList();

            guestNames.Count.Should().Be(0);
        }

        [Fact]
        public void CalculateTotalNightPerTravelAgentWithCorrectDate_ShouldReturnNumberOfNights()
        {
            var groups = GenerateTestNights();

            var actualTravelInfo = InvoiceExtensions.CalculateNumberOfNightsByTravelAgentWithDate(groups, 2015).ToList();

            var expextedTravelInfo = new List<TravelAgentInfo>
            {
                new TravelAgentInfo {TravelAgent = "Agent1", TotalNumberOfNights = 7 },
                new TravelAgentInfo {TravelAgent = "Agent2", TotalNumberOfNights = 4 },
                new TravelAgentInfo {TravelAgent = "Agent3", TotalNumberOfNights = 0}
            };

            actualTravelInfo.Count.Should().Be(3);
            actualTravelInfo.Should().BeEquivalentTo(expextedTravelInfo);
        }

        [Fact]
        public void CalculateTotalNightPerTravelAgentWithWrongDate_ShouldNotReturnNumberOfNights()
        {
            var groups = GenerateTestNights();

            var actualTravelInfo = InvoiceExtensions.CalculateNumberOfNightsByTravelAgentWithDate(groups, 2024).ToList();

            actualTravelInfo.Count.Should().Be(0);
        }

        private static List<InvoiceGroup> GenerateTesInvoideGroups()
        {
            var groups = new List<InvoiceGroup>
            {
                new InvoiceGroup
                {
                    Invoices = new List<Invoice>()
                    {
                        new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent1", GuestName = "John" } } },
                        new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent1", GuestName = "Mark" } } },
                        new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent1", GuestName = "Peter" } } },
                        new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent1", GuestName = "Nick" } } }
                    },
                },
                new InvoiceGroup
                {
                    Invoices = new List<Invoice>() { new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent2", GuestName = "John" } } } }
                },
            };

            return groups;
        }

        private static List<InvoiceGroup> GenerateUniqueTesInvoideGroups()
        {
            var groups = new List<InvoiceGroup>
            {
                new InvoiceGroup
                {
                    Invoices = new List<Invoice>()
                    {
                        new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent1", GuestName = "John" } } },
                        new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent1", GuestName = "Mark" } } },
                        new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent1", GuestName = "Peter" } } },
                        new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent1", GuestName = "Den" } } }
                    },
                },
                new InvoiceGroup
                {
                    Invoices = new List<Invoice>() { new Invoice { Observations = new List<Observation>{ new Observation { TravelAgent = "Agent2", GuestName = "Nick" } } } }
                }
            };

            return groups;
        }

        private static List<InvoiceGroup> GenerateTestNights()
        {
            var groups = new List<InvoiceGroup>
            {
                new InvoiceGroup
                {
                    IssueDate = new DateTime(2015, 10, 8),
                    Invoices = new List<Invoice>()
                    {
                        new() {
                            Observations =
                            [
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 }
                            ]
                        },
                        new() {
                            Observations =
                            [
                                new() { TravelAgent = "Agent2", NumberOfNights = 2 },
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 }
                            ]
                        },
                        new() {
                            Observations =
                            [
                                new() { TravelAgent = "Agent3" },
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 }
                            ]
                        },
                    },
                },
                
                new InvoiceGroup
                {
                    IssueDate = new DateTime(2023, 1, 1),
                    Invoices = new List<Invoice>()
                    {
                        new() {
                            Observations =
                            [
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 }
                            ]
                        },
                        new() {
                            Observations =
                            [
                                new() { TravelAgent = "Agent2", NumberOfNights = 2 },
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 }
                            ]
                        },
                        new() {
                            Observations =
                            [
                                new() { TravelAgent = "Agent3" },
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 }
                            ]
                        },
                    },
                },

                new InvoiceGroup
                {
                    IssueDate = new DateTime(2015, 1, 10),
                    Invoices = new List<Invoice>()
                    {
                        new() {
                            Observations =
                            [
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 },
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 }
                            ]
                        },
                        new() {
                            Observations =
                            [
                                new() { TravelAgent = "Agent2", NumberOfNights = 2 },
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 }
                            ]
                        },
                        new() {
                            Observations =
                            [
                                new() { TravelAgent = "Agent3" },
                                new() { TravelAgent = "Agent1", NumberOfNights = 1 }
                            ]
                        },
                    },
                }
            };

            return groups;
        }
    }
}