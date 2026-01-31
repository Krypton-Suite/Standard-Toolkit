#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace BogusData;

using Bogus;

public static class BogusGenerator
{
    public static List<BogusUser> GenerateUserList(int count)
    {
        var fakerInstance = new Faker<BogusUser>()
            .RuleFor(user => user.Id, faker => faker.IndexFaker + 1)
            .RuleFor(user => user.Name, faker => faker.Name.FullName())
            .RuleFor(user => user.Country, faker => faker.Address.Country())
            .RuleFor(user => user.City, faker => faker.Address.City())
            .RuleFor(user => user.Street, faker => faker.Address.StreetName())
            .RuleFor(user => user.HouseNumber, faker => faker.Address.Random.Number().ToString())
            .RuleFor(user => user.ZipCode, faker => faker.Address.ZipCode());

        return fakerInstance.Generate(count);
    }

    public static string GenerateLoremIpsumLines(int numberOfLines = 10)
    {
        var fakerInstance = new Faker();
        return fakerInstance.Lorem.Paragraph(numberOfLines);
    }
}
