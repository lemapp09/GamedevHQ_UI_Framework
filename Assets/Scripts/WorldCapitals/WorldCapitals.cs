using System;
using System.Collections.Generic;
using LemApperson.StateMaps;
using MyNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LemApperson.WorldCapitals
{
    public class WorldCapitals : MonoSingleton<WorldCapitals>
    {
        // Country Index, Country Flag Index, Country Name,. Country Capital
        private List<(int, string, string, int, int, int)> _worldCapitals =
            new List<(int, string, string, int, int, int)>();
        [SerializeField] private GameObject _countryTilePrefab, _capitalTilePrefab, _columnA, _columnB;
        [SerializeField] private Score _score;
        [Tooltip("The continent Code assigned to each continent")]
        [Range(0,5)]
        [SerializeField] private int _continentCode = 0;
        private Color[] _colors;
        private Sprite[] _spriteSheet;
        private List<int> _allCountries;
        private Color[] _colorOrder;
        private int[] _countryList, _capitalList;
        
        private void Start() {
            PopulateCapitalList();
            LoadSpriteSheets();
            _colors = new Color[16];
            _colorOrder = new Color[280];
            PopulateColors();
            _allCountries = ReturnListByContinent(_continentCode);
            (_countryList, _capitalList) = SelectCountries();
            PopulateColA(_countryList);
            PopulateColB(_capitalList);
        }

        public void CapitalMatched() {
            _score.UpdateCapitalScore(10);
        }
        
        private (int[], int[]) SelectCountries()
        {
            int[] temp1 =  new int[8];
            int[] temp2 = new int[8];
            for (int i = 0; i < 8; i++) {
                temp1[i] = temp2[i] = _allCountries[Random.Range(0, _allCountries.Count)];
            }
            temp1 = Shuffle(temp1);
            temp2 = Shuffle(temp2);
            return (temp1, temp2);
        }

        private void PopulateColA(int[] _countryList)
        {
            _countryList = Shuffle(_countryList);
            for (int i = 0; i < 8; i++)
            {
                (int spriteSheet, int SpriteNumber) = ReturnSpriteFileInfo(_countryList[i]);
                GameObject temp = Instantiate(_countryTilePrefab, _columnA.transform.position, Quaternion.identity, _columnA.transform);
                CountryTile tempCountryTile = temp.GetComponent<CountryTile>();
                                // (int countryIndex, int tileIndex ,string countryName,Sprite sprite, Color dotColor)
                tempCountryTile.SetCountryData(_countryList[i], i ,ReturnCountry(_countryList[i]),
                    GetSpriteFromSpriteSheet(_countryList[i] , spriteSheet, SpriteNumber), _colors[i]);
                tempCountryTile.name = "CountryTile_" + i.ToString();
                _colorOrder[_countryList[i]] = _colors[i];
            }
        }

        private void PopulateColB(int[] _capitalList)
        {
            _capitalList = Shuffle(_capitalList);
            for (int i = 0; i < 8; i++)
            {
                GameObject temp = Instantiate(_capitalTilePrefab, _columnB.transform.position, Quaternion.identity, _columnB.transform);
                CapitalTile tempCapitalTile = temp.GetComponent<CapitalTile>();
                Color tempColor = _colorOrder[_capitalList[i]];
                // (string capitalName, int countryIndex, Color dotColor)
                tempCapitalTile.SetCapitalData(ReturnCapital(_capitalList[i]), _capitalList[i], tempColor);
                tempCapitalTile.name = "CapitalTile_" + i.ToString();
            }
        }

        private void PopulateColors()
        {
            for (int i = 0; i < 8; i++)
            {
                string seed = Time.time.ToString ();
                System.Random random = new System.Random (seed.GetHashCode ());
                _colors[i] = new Color(
                    Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)
                );
            }
        }

        public List<int> ReturnListByContinent(int continentCode) {
            List<int> temp = new List<int>();
            for (int i = 0; i < _worldCapitals.Count; i++) {
                // Contry Code, Country Name, Continent Code, Sprite Sheet Number, Sprite Number
                (int firstInt, string firstString1, string firstString2, int firstInt2, int firstInt3,int firstInt4) = _worldCapitals[i];
                if (firstInt2 == continentCode) {
                    temp.Add(firstInt);
                }
            }
            return temp;
        }

        public string ReturnCapital(int indexNumber)
        {
            for (int i = 0; i < _worldCapitals.Count; i++) {
                (int firstInt, string firstString1, string firstString2, int firstInt2, int firstInt3,int firstInt4) = _worldCapitals[i];
                if (firstInt == indexNumber) {
                    return firstString2;
                }
            }
            return "";
        }

        public string ReturnCountry(int indexNumber)
        {
                for (int i = 0; i < _worldCapitals.Count; i++) {
                    (int firstInt, string firstString1, string firstString2, int firstInt2, int firstInt3,int firstInt4) = _worldCapitals[i];
                    if (firstInt == indexNumber) {
                        return firstString1;
                    }
                }
                return "";
        }

        public (int, int) ReturnSpriteFileInfo(int indexNumber) { 
            for (int i = 0; i < _worldCapitals.Count; i++) {
                (int firstInt, string firstString1, string firstString2, int firstInt2, int firstInt3,int firstInt4) = _worldCapitals[i];
                if (firstInt == indexNumber) {
                    return (firstInt3,firstInt4);
                }
            }
            return (0,0);
        }
        
        public Sprite GetSpriteFromSpriteSheet(int countryCode,  int spriteSheetIndex, int spriteIndex)
        {
            if (spriteSheetIndex == 0) {
                return _spriteSheet[0];
            } else  if (spriteSheetIndex == 1) {
                return _spriteSheet[spriteIndex];
            } else  {
                return _spriteSheet[spriteIndex + 140];
            }
        }
        
        public void LoadSpriteSheets() {
            _spriteSheet = Resources.LoadAll<Sprite>("Textures/WorldFlags");
        }

        private void PopulateCapitalList()
        {
            _worldCapitals.Add((6, "Algeria", "Algiers", 0, 1, 0));
            _worldCapitals.Add((10, "Angola", "Luanda", 0, 2, 7));
            _worldCapitals.Add((25, "Benin", "Porto Novo, Cotonou", 0, 1, 22));
            _worldCapitals.Add((30, "Botswana", "Gaborone", 0, 1, 24));
            _worldCapitals.Add((34, "Burkina Faso", "Ouagadougou", 0, 2, 24));
            _worldCapitals.Add((35, "Burundi", "Gitega", 0, 1, 29));
            _worldCapitals.Add((38, "Cameroon (also spelled Cameroun)", "Yaoundé", 0, 2, 28));
            _worldCapitals.Add((41, "Cape Verde", "Praia", 0, 1, 45));
            _worldCapitals.Add((42, "Central African Republic", "Bangui", 0, 1, 44));
            _worldCapitals.Add((43, "Chad (Tchad)", "N'Djamena", 0, 1, 31));
            _worldCapitals.Add((47, "Comoros", "Moroni", 0, 1, 32));
            _worldCapitals.Add((50, "Côte d'Ivoire (Ivory Coast)", "Yamoussoukro", 0, 2, 37));
            _worldCapitals.Add((55, "Democratic Republic of the Congo (Zaire)", "Kinshasa", 0, 2, 34));
            _worldCapitals.Add((57, "Djibouti", "Djibouti", 0, 2, 44));
            _worldCapitals.Add((62, "Egypt (Misr)", "Cairo", 0, 1, 51));
            _worldCapitals.Add((65, "Equatorial Guinea", "Malabo", 0, 1, 53));
            _worldCapitals.Add((66, "Eritrea", "Asmara", 0, 1, 56));
            _worldCapitals.Add((68, "Ethiopia (Abyssinia)", "Addis Ababa", 0, 1, 55));
            _worldCapitals.Add((74, "Gabon", "Libreville", 0, 1, 67));
            _worldCapitals.Add((77, "Ghana", "Accra", 0, 1, 60));
            _worldCapitals.Add((83, "Guinea", "Conakry", 0, 2, 66));
            _worldCapitals.Add((84, "Guinea-Bissau", "Bissau", 0, 1, 66));
            _worldCapitals.Add((102, "Kenya", "Nairobi", 0, 1, 84));
            _worldCapitals.Add((110, "Lesotho", "Maseru", 0, 1, 91));
            _worldCapitals.Add((111, "Liberia", "Monrovia", 0, 1, 99));
            _worldCapitals.Add((112, "Libya", "Tripoli", 0, 1, 96));
            _worldCapitals.Add((117, "Madagascar", "Antananarivo", 0, 2, 99));
            _worldCapitals.Add((120, "Mali", "Bamako", 0, 1, 100));
            _worldCapitals.Add((123, "Mauritania", "Nouakchott", 0, 2, 109));
            _worldCapitals.Add((124, "Mauritius", "Port Louis", 0, 1, 104));
            _worldCapitals.Add((130, "Morocco (Al Maghrib)", "Rabat", 0, 1, 108));
            _worldCapitals.Add((131, "Mozambique", "Maputo", 0, 1, 112));
            _worldCapitals.Add((133, "Namibia", "Windhoek", 0, 1, 110));
            _worldCapitals.Add((138, "Niger", "Niamey", 0, 1, 120));
            _worldCapitals.Add((139, "Nigeria", "Abuja", 0, 2, 114));
            _worldCapitals.Add((154, "Republic of the Congo", "Brazzaville", 0, 1, 42));
            _worldCapitals.Add((157, "Rwanda", "Kigali", 0, 2, 128));
            _worldCapitals.Add((160, "São Tomé and Príncipe", "São Tomé", 0, 2, 134));
            _worldCapitals.Add((162, "Senegal", "Dakar", 0, 2, 11));
            _worldCapitals.Add((164, "Seychelles", "Victoria", 0, 1, 138));
            _worldCapitals.Add((165, "Sierra Leone", "Freetown", 0, 1, 133));
            _worldCapitals.Add((170, "Somalia", "Mogadishu", 0, 2, 63));
            _worldCapitals.Add((171, "South Africa", "Pretoria", 0, 1, 136));
            _worldCapitals.Add((177, "Swaziland (Eswatini)", "Mbabane", 0, 2, 131));
            _worldCapitals.Add((183, "Tanzania", "Dodoma", 0, 2, 113));
            _worldCapitals.Add((185, "The Gambia", "Banjul", 0, 2, 64));
            _worldCapitals.Add((188, "Togo", "Lome", 0, 1, 130));
            _worldCapitals.Add((191, "Tunisia", "Tunis", 0, 2, 32));
            _worldCapitals.Add((195, "Uganda", "Kampala", 0, 2, 113));
            _worldCapitals.Add((206, "Western Sahara", "El Aaiún (disputed)", 0, 2, 121));
            _worldCapitals.Add((208, "Zambia", "Lusaka", 0, 2, 123));
            _worldCapitals.Add((3, "Afghanistan", "Kabul", 1, 1, 4));
            _worldCapitals.Add((13, "Armenia", "Yerevan", 1, 1, 13));
            _worldCapitals.Add((16, "Azerbaijan", "Baku", 1, 1, 10));
            _worldCapitals.Add((18, "Bahrain", "Manama", 1, 2, 15));
            _worldCapitals.Add((19, "Bangladesh (বাংলাদেশ)", "Dhaka (ঢাকা)", 1, 2, 16));
            _worldCapitals.Add((21, "Bashkortstan", "Ufa", 1, 1, 17));
            _worldCapitals.Add((27, "Bhutan", "Thimphu", 1, 1, 15));
            _worldCapitals.Add((32, "Brunei", "Bandar Seri Begawan", 1, 2, 26));
            _worldCapitals.Add((36, "Buryatia", "	Ulan-Ude", 1, 1, 18));
            _worldCapitals.Add((37, "Cambodia (Kampuchea)", "Phnom Penh", 1, 2, 29));
            _worldCapitals.Add((45, "China", "Beijing", 1, 1, 36));
            _worldCapitals.Add((60, "East Timor (Timor Leste)", "Dili", 1, 2, 47));
            _worldCapitals.Add((75, "Georgia", "Tbilisi", 1, 1, 61));
            _worldCapitals.Add((88, "Hong Kong", "Hong Kong", 1, 1, 70));
            _worldCapitals.Add((91, "India", "New Delhi", 1, 1, 72));
            _worldCapitals.Add((92, "Indonesia", "Jakarta", 1, 1, 77));
            _worldCapitals.Add((93, "Iran", "Tehran", 1, 1, 76));
            _worldCapitals.Add((94, "Iraq", "Baghdad", 1, 1, 73));
            _worldCapitals.Add((95, "Ireland", "Dublin", 1, 1, 79));
            _worldCapitals.Add((96, "Israel", "Jerusalem", 1, 2, 74));
            _worldCapitals.Add((99, "Japan", "Tokyo", 1, 2, 78));
            _worldCapitals.Add((100, "Jordan (Al Urdun)", "Amman", 1, 2, 79));
            _worldCapitals.Add((101, "Kazakhstan", "Nur-Sultan", 1, 2, 89));
            _worldCapitals.Add((105, "Kuwait", "Kuwait City", 1, 2, 85));
            _worldCapitals.Add((106, "Kyrgyzstan", "Bishkek", 1, 1, 89));
            _worldCapitals.Add((107, "Laos", "Vientiane", 1, 1, 90));
            _worldCapitals.Add((109, "Lebanon (Lubnan)", "Beirut", 1, 1, 93));
            _worldCapitals.Add((118, "Malaysia", "Kuala Lumpur", 1, 2, 97));
            _worldCapitals.Add((119, "Maldives", "Male", 1, 1, 96));
            _worldCapitals.Add((128, "Mongolia", "Ulaanbaatar", 1, 1, 109));
            _worldCapitals.Add((132, "Myanmar (Burma)", "Naypyidaw", 1, 1, 111));
            _worldCapitals.Add((134, "Nepal", "Kathmandu", 1, 1, 117));
            _worldCapitals.Add((140, "North Korea", "Pyongyang", 1, 1, 86));
            _worldCapitals.Add((142, "Oman", "Muscat", 1, 1, 121));
            _worldCapitals.Add((143, "Pakistan", "Islamabad", 1, 1, 127));
            _worldCapitals.Add((145, "Palestine", "Ramallah", 1, 1, 122));
            _worldCapitals.Add((152, "Qatar", "Doha", 1, 2, 127));
            _worldCapitals.Add((161, "Saudi Arabia", "Riyadh", 1, 2, 139));
            _worldCapitals.Add((166, "Singapore", "Singapore", 1, 2, 61));
            _worldCapitals.Add((173, "South Korea", "Seoul", 1, 1, 88));
            _worldCapitals.Add((175, "Sri Lanka", "Sri Jayawardenapura Kotte", 1, 2, 20));
            _worldCapitals.Add((180, "Syria", "Damascus", 1, 2, 73));
            _worldCapitals.Add((181, "Taiwan", "Taipei", 1, 2, 23));
            _worldCapitals.Add((182, "Tajikistan", "Dushanbe", 1, 2, 102));
            _worldCapitals.Add((184, "Thailand (Muang Thai)", "Bangkok", 1, 2, 22));
            _worldCapitals.Add((186, "The Maldives", "Malé", 1, 2, 96));
            _worldCapitals.Add((187, "The Philippines", "Manila", 1, 2, 124));
            _worldCapitals.Add((192, "Turkey (Türkiye)", "Ankara", 1, 2, 83));
            _worldCapitals.Add((193, "Turkmenistan", "Aşgabat", 1, 1, 131));
            _worldCapitals.Add((196, "United Arab Emirates", "Abu Dhabi", 1, 2, 111));
            _worldCapitals.Add((201, "Uzbekistan", "Tashkent", 1, 2, 91));
            _worldCapitals.Add((205, "Vietnam", "Hanoi", 1, 2, 43));
            _worldCapitals.Add((207, "Yemen", "Sana'a", 1, 2, 122));
            _worldCapitals.Add((4, "Albania (Shqipëria)", "Tirana", 2, 1, 3));
            _worldCapitals.Add((9, "Andorra", "Andorra la Vella", 2, 1, 8));
            _worldCapitals.Add((15, "Austria (Österreich)", "Vienna", 2, 1, 14));
            _worldCapitals.Add((22, "Belarus (Беларусь)", "Minsk", 2, 1, 28));
            _worldCapitals.Add((23, "Belgium", "Brussels", 2, 1, 19));
            _worldCapitals.Add((29, "Bosnia and Herzegovina", "Sarajevo", 2, 1, 26));
            _worldCapitals.Add((33, "Bulgaria (България)", "Sofia", 2, 2, 25));
            _worldCapitals.Add((51, "Croatia (Hrvatska)", "Zagreb", 2, 2, 39));
            _worldCapitals.Add((53, "Cyprus (Κύπρος)", "Nicosia", 2, 1, 35));
            _worldCapitals.Add((54, "Czech Republic (Česko)", "Prague", 2, 1, 34));
            _worldCapitals.Add((56, "Denmark (Danmark)", "Copenhagen", 2, 1, 48));
            _worldCapitals.Add((64, "England", "London", 2, 1, 54));
            _worldCapitals.Add((67, "Estonia (Eesti)", "Tallinn", 2, 2, 49));
            _worldCapitals.Add((72, "Finland (Suomi)", "Helsinki", 2, 1, 58));
            _worldCapitals.Add((73, "France", "Paris", 2, 2, 56));
            _worldCapitals.Add((76, "Germany (Deutschland)", "Berlin", 2, 1, 64));
            _worldCapitals.Add((78, "Gilbraltar", "Gilbraltar", 2, 1, 69));
            _worldCapitals.Add((79, "Greece (Ελλάδα)", "Athens", 2, 1, 63));
            _worldCapitals.Add((89, "Hungary (Magyarország)", "Budapest", 2, 2, 68));
            _worldCapitals.Add((90, "Iceland", "Reykjavik", 2, 1, 75));
            _worldCapitals.Add((97, "Italy (Italia)", "Rome", 2, 1, 78));
            _worldCapitals.Add((104, "Kosovo", "Pristina", 2, 2, 88));
            _worldCapitals.Add((108, "Latvia (Latvija)", "Riga", 2, 1, 97));
            _worldCapitals.Add((113, "Liechtenstein", "Vaduz", 2, 1, 92));
            _worldCapitals.Add((114, "Lithuania (Lietuva)", "Vilnius", 2, 1, 95));
            _worldCapitals.Add((115, "Luxembourg", "Luxembourg City", 2, 2, 94));
            _worldCapitals.Add((116, "Macedonia", "Skopje", 2, 1, 105));
            _worldCapitals.Add((121, "Malta", "Valletta", 2, 2, 95));
            _worldCapitals.Add((126, "Moldova", "Chisinau", 2, 2, 108));
            _worldCapitals.Add((127, "Monaco", "Monte Carlo Quarter", 2, 1, 107));
            _worldCapitals.Add((129, "Montenegro (Crna Gora, Црна Гора)", "Podgorica", 2, 2, 105));
            _worldCapitals.Add((135, "Netherlands", "Amsterdam", 2, 1, 118));
            _worldCapitals.Add((141, "Norway (Norge)", "Oslo", 2, 2, 116));
            _worldCapitals.Add((149, "Poland (Polska)", "Warsaw", 2, 1, 126));
            _worldCapitals.Add((150, "Portugal", "Lisbon", 2, 1, 124));
            _worldCapitals.Add((153, "Republic of Ireland (Éire)", "Dublin", 2, 1, 79));
            _worldCapitals.Add((155, "Romania", "Bucharest", 2, 2, 132));
            _worldCapitals.Add((156, "Russia", "Moscow", 2, 2, 129));
            _worldCapitals.Add((159, "San Marino", "San Marino", 2, 2, 138));
            _worldCapitals.Add((163, "Serbia (Србија)", "Belgrade", 2, 1, 132));
            _worldCapitals.Add((167, "Slovakia (Slovensko)", "Bratislava", 2, 2, 62));
            _worldCapitals.Add((168, "Slovenia (Slovenija)", "Ljubljana", 2, 1, 134));
            _worldCapitals.Add((174, "Spain (España)", "Madrid", 2, 1, 135));
            _worldCapitals.Add((178, "Sweden (Sverige)", "Stockholm", 2, 2, 21));
            _worldCapitals.Add((179, "Switzerland", "Bern", 2, 2, 70));
            _worldCapitals.Add((197, "United Kingdom", "London", 2, 1, 54));
            _worldCapitals.Add((203, "Vatican City", "Vatican City", 2, 2, 93));
            _worldCapitals.Add((11, "Aruba", "Oranjestad", 3, 1, 11));
            _worldCapitals.Add((17, "Bahamas", "Nassau", 3, 2, 14));
            _worldCapitals.Add((20, "Barbados", "Bridgetown", 3, 2, 17));
            _worldCapitals.Add((24, "Belize", "Belmopan", 3, 1, 16));
            _worldCapitals.Add((26, "Bermuda", "Hamilton", 3, 1, 20));
            _worldCapitals.Add((40, "Canada", "Ottawa", 3, 1, 37));
            _worldCapitals.Add((39, "Cayman Islands", "George Town", 3, 1, 38));
            _worldCapitals.Add((49, "Costa Rica", "San José", 3, 2, 36));
            _worldCapitals.Add((52, "Cuba", "Havana", 3, 2, 35));
            _worldCapitals.Add((58, "Dominica", "Roseau", 3, 1, 49));
            _worldCapitals.Add((59, "Dominican Republic", "Santo Domingo", 3, 1, 47));
            _worldCapitals.Add((63, "El Salvador", "San Salvador", 3, 1, 52));
            _worldCapitals.Add((80, "Greenland (territory of Denmark)", "Nuuk", 3, 2, 59));
            _worldCapitals.Add((81, "Grenada", "St. George's", 3, 1, 58));
            _worldCapitals.Add((82, "Guatemala", "Guatemala City", 3, 1, 62));
            _worldCapitals.Add((86, "Haiti", "Port-au-Prince", 3, 1, 71));
            _worldCapitals.Add((87, "Honduras", "Tegucigalpa", 3, 2, 67));
            _worldCapitals.Add((98, "Jamaica", "Kingston", 3, 2, 77));
            _worldCapitals.Add((125, "Mexico", "Mexico City", 3, 2, 98));
            _worldCapitals.Add((137, "Nicaragua", "Managua", 3, 2, 115));
            _worldCapitals.Add((151, "Puerto Rico", "San Juan", 3, 2, 125));
            _worldCapitals.Add((198, "United States of America", "Washington, District of Columbia", 3, 2, 130));
            _worldCapitals.Add((200, "US Virgin Islands", "Charlotte Amalie", 3, 2, 43));
            _worldCapitals.Add((14, "Australia", "Canberra", 4, 2, 5));
            _worldCapitals.Add((70, "Federated States of Micronesia", "Palikir", 4, 2, 106));
            _worldCapitals.Add((71, "Fiji", "Suva", 4, 2, 55));
            _worldCapitals.Add((103, "Kiribati", "South Tarawa", 4, 1, 87));
            _worldCapitals.Add((122, "Marshall Islands", "Majuro", 4, 1, 103));
            _worldCapitals.Add((136, "New Zealand", "Wellington", 4, 1, 114));
            _worldCapitals.Add((144, "Palau", "Ngerulmud", 4, 1, 129));
            _worldCapitals.Add((146, "Papua New Guinea", "Port Moresby", 4, 1, 123));
            _worldCapitals.Add((158, "Samoa", "Apia", 4, 2, 52));
            _worldCapitals.Add((169, "Solomon Islands", "Honiara", 4, 1, 137));
            _worldCapitals.Add((189, "Tonga", "Nuku'alofa", 4, 2, 101));
            _worldCapitals.Add((194, "Tuvalu", "Funafuti", 4, 2, 110));
            _worldCapitals.Add((202, "Vanuatu", "Port Vila", 4, 2, 92));
            _worldCapitals.Add((12, "Argentina", "Buenos Aires", 5, 1, 5));
            _worldCapitals.Add((28, "Bolivia", "Sucré", 5, 1, 25));
            _worldCapitals.Add((31, "Brazil (Brasil)", "Brasília", 5, 1, 21));
            _worldCapitals.Add((44, "Chile", "Santiago", 5, 1, 30));
            _worldCapitals.Add((46, "Colombia", "Bogotá", 5, 1, 43));
            _worldCapitals.Add((61, "Ecuador", "Quito", 5, 2, 48));
            _worldCapitals.Add((69, "Falkland Islands", "Stanley", 5, 2, 46));
            _worldCapitals.Add((85, "Guyana", "Georgetown", 5, 2, 65));
            _worldCapitals.Add((147, "Paraguay", "Asunción", 5, 1, 128));
            _worldCapitals.Add((148, "Peru", "Lima", 5, 1, 125));
            _worldCapitals.Add((172, "South Georgia and the South Sandwich Islands", "King Edward Point", 5, 1, 139));
            _worldCapitals.Add((176, "Suriname", "Paramaribo", 5, 2, 71));
            _worldCapitals.Add((190, "Trinidad and Tobago", "Port of Spain", 5, 2, 81));
            _worldCapitals.Add((199, "Uruguay", "Montevideo", 5, 2, 90));
            _worldCapitals.Add((204, "Venezuela", "Caracas", 5, 2, 40));
        }
        
        //for shuffle number from array
        private int[] Shuffle(int[] array)
        {
            int p = array.Length;
            for (int n = p - 1; n > 0; n--)
            {
                // Change Random seed value
                UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
                int r = UnityEngine.Random.Range(0, n);
                int t = array[r];
                array[r] = array[n];
                array[n] = t;
            }
            return array;
        }
    }
}

