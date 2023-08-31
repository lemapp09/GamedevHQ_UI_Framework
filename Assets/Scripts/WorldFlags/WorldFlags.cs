using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LemApperson.WorldFlags
{
    public class WorldFlags : MonoBehaviour
    {
        // Need a variable of <int, string, string, string>
        private List<(int, string, int, int, int)> _worldFlags = new List<(int, string, int, int, int)>();
        [SerializeField] private GameObject _prefabWFTile;
        [Tooltip("The continent Code assigned to each continent")]
        [Range(0,5)]
        [SerializeField] private int _continentCode = 0;
        private int[] displayOrder, tileOrder;
        private List<int> allContinentFlags;
        private Sprite[] spriteSheetSprite01, spriteSheetSprite02;

        private void Start() {
            LoadSpriteSheets();
            PopulateFlagList();
            PopulateGridWithFlagTiles();
            allContinentFlags = ReturnListByType(_continentCode);
            GenerateRandomList();
            GenerateListTiles();
            PopulateGridWithFlagTiles();

        }

        private void GenerateRandomList() {
            tileOrder = new int[36];
            for (int i = 0; i < 36; i++) {
                // populate array in sequential order
                tileOrder[i] = i;
            }

            for (int i = 0; i < tileOrder.Length; i++) {
                // go through each element and switch with another
                int temp = tileOrder[i];
                int randomIndex = Random.Range(i, tileOrder.Length);
                tileOrder[i] = tileOrder[randomIndex];
                tileOrder[randomIndex] = temp;
            }
        }

        private void GenerateListTiles() {
            displayOrder = new int[36];
            for (int i = 0; i < 36; i += 2) {
                int randomFlagIIndex = Random.Range(0, allContinentFlags.Count);
                displayOrder[tileOrder[i]] = allContinentFlags[ randomFlagIIndex];
                displayOrder[tileOrder[i + 1]] = allContinentFlags [randomFlagIIndex];
            }
        }

        private void PopulateGridWithFlagTiles() {
            for (int i = 0; i < 36; i++) {
                GameObject holderObject = Instantiate(_prefabWFTile, this.transform.position, Quaternion.identity );
                holderObject.transform.SetParent(this.transform);
                var _tempTile = holderObject.GetComponent<WorldFlagTiles>();
                (int _tempSheetNumber ,  int _tempSpriteNumber ) = ReturnSpriteFileName(displayOrder[i]);
                _tempTile.SetUp(ReturnCountryName(displayOrder[i]), 
                    _tempSheetNumber, _tempSpriteNumber, displayOrder[i]);
                holderObject.name = "FlagTile" + i;
                _tempTile.SetFlagImage(GetSpriteFromSpriteSheet(displayOrder[i],_tempSheetNumber, _tempSpriteNumber));
            }
        }

        public List<int> ReturnListByType(int continentCode) {
            List<int> temp = new List<int>();
            for (int i = 0; i < _worldFlags.Count; i++) {
                // Contry Code, Country Name, Continent Code, Sprite Sheet Number, Sprite Number
                (int firstInt, string firstString1, int firstInt2, int firstInt3,int firstInt4) = _worldFlags[i];
                if (firstInt2 == continentCode) {
                    temp.Add(firstInt);
                }
            }
            return temp;
        }

        public string ReturnCountryName(int indexNumber) { 
            for (int i = 0; i < _worldFlags.Count; i++) {
                (int firstInt, string firstString1, int firstInt2, int firstInt3,int firstInt4) = _worldFlags[i];
                if (firstInt == indexNumber) {
                    return firstString1;
                }
            }
            return "";
        }
        
        public (int, int) ReturnSpriteFileName(int indexNumber) { 
            for (int i = 0; i < _worldFlags.Count; i++) {
                (int firstInt, string firstString1, int firstInt2, int firstInt3,int firstInt4) = _worldFlags[i];
                if (firstInt == indexNumber) {
                    return (firstInt3,firstInt4);
                }
            }
            return (0,0);
        }

        public void LoadSpriteSheets(){
            spriteSheetSprite01 = Resources.LoadAll<Sprite>("Textures/WorldFlags");  
        }

        public Sprite GetSpriteFromSpriteSheet(int countryCode,  int spriteSheetIndex, int spriteIndex)
        {
            if (spriteSheetIndex == 0) {
                return spriteSheetSprite01[0];
            } else  if (spriteSheetIndex == 1) {
                return spriteSheetSprite01[spriteIndex];
            } else  {
                return spriteSheetSprite01[spriteIndex + 140];
            }
        }

        private void PopulateFlagList()
        {           
            #region PopulateFlags
            _worldFlags.Add((6 , "Algeria - Algiers" , 0 , 1 , 0 ));
            _worldFlags.Add((10 , "Angola - Luanda" , 0 , 2 , 7 ));
            _worldFlags.Add((25 , "Benin - Porto Novo, Cotonou" , 0 , 1 , 22 ));
            _worldFlags.Add((30 , "Botswana - Gaborone" , 0 , 1 , 24 ));
            _worldFlags.Add((34 , "Burkina Faso - Ouagadougou" , 0 , 2 , 24 ));
            _worldFlags.Add((35 , "Burundi - Gitega" , 0 , 1 , 29 ));
            _worldFlags.Add((38 , "Cameroon (also spelled Cameroun) - Yaoundé" , 0 , 2 , 28 ));
            _worldFlags.Add((41 , "Cape Verde - Praia" , 0 , 1 , 45 ));
            _worldFlags.Add((42 , "Central African Republic - Bangui" , 0 , 1 , 44 ));
            _worldFlags.Add((43 , "Chad (Tchad) - N'Djamena" , 0 , 1 , 31 ));
            _worldFlags.Add((47 , "Comoros - Moroni" , 0 , 1 , 32 ));
            _worldFlags.Add((50 , "Côte d'Ivoire (Ivory Coast) - Yamoussoukro" , 0 , 2 , 37 ));
            _worldFlags.Add((55 , "Democratic Republic of the Congo (Zaire) - Kinshasa" , 0 , 2 , 34 ));
            _worldFlags.Add((57 , "Djibouti - Djibouti" , 0 , 2 , 44 ));
            _worldFlags.Add((62 , "Egypt (Misr) - Cairo" , 0 , 1 , 51 ));
            _worldFlags.Add((65 , "Equatorial Guinea - Malabo" , 0 , 1 , 53 ));
            _worldFlags.Add((66 , "Eritrea - Asmara" , 0 , 1 , 56 ));
            _worldFlags.Add((68 , "Ethiopia (Abyssinia) - Addis Ababa" , 0 , 1 , 55 ));
            _worldFlags.Add((74 , "Gabon - Libreville" , 0 , 1 , 67 ));
            _worldFlags.Add((77 , "Ghana - Accra" , 0 , 1 , 60 ));
            _worldFlags.Add((83 , "Guinea - Conakry" , 0 , 2 , 66 ));
            _worldFlags.Add((84 , "Guinea-Bissau - Bissau" , 0 , 1 , 66 ));
            _worldFlags.Add((102 , "Kenya - Nairobi" , 0 , 1 , 84 ));
            _worldFlags.Add((110 , "Lesotho - Maseru" , 0 , 1 , 91 ));
            _worldFlags.Add((111 , "Liberia - Monrovia" , 0 , 1 , 99 ));
            _worldFlags.Add((112 , "Libya - Tripoli" , 0 , 1 , 96 ));
            _worldFlags.Add((117 , "Madagascar - Antananarivo" , 0 , 2 , 99 ));
            _worldFlags.Add((120 , "Mali - Bamako" , 0 , 1 , 100 ));
            _worldFlags.Add((123 , "Mauritania - Nouakchott" , 0 , 2 , 109 ));
            _worldFlags.Add((124 , "Mauritius - Port Louis" , 0 , 1 , 104 ));
            _worldFlags.Add((130 , "Morocco (Al Maghrib) - Rabat" , 0 , 1 , 108 ));
            _worldFlags.Add((131 , "Mozambique - Maputo" , 0 , 1 , 112 ));
            _worldFlags.Add((133 , "Namibia - Windhoek" , 0 , 1 , 110 ));
            _worldFlags.Add((138 , "Niger - Niamey" , 0 , 1 , 120 ));
            _worldFlags.Add((139 , "Nigeria - Abuja" , 0 , 2 , 114 ));
            _worldFlags.Add((154 , "Republic of the Congo - Brazzaville" , 0 , 1 , 42 ));
            _worldFlags.Add((157 , "Rwanda - Kigali" , 0 , 2 , 128 ));
            _worldFlags.Add((160 , "São Tomé and Príncipe - São Tomé" , 0 , 2 , 134 ));
            _worldFlags.Add((162 , "Senegal - Dakar" , 0 , 2 , 11 ));
            _worldFlags.Add((164 , "Seychelles - Victoria" , 0 , 1 , 138 ));
            _worldFlags.Add((165 , "Sierra Leone - Freetown" , 0 , 1 , 133 ));
            _worldFlags.Add((170 , "Somalia - Mogadishu" , 0 , 2 , 63 ));
            _worldFlags.Add((171 , "South Africa - Pretoria" , 0 , 1 , 136 ));
            _worldFlags.Add((177 , "Swaziland (Eswatini) - Mbabane" , 0 , 2 , 131 ));
            _worldFlags.Add((183 , "Tanzania - Dodoma" , 0 , 2 , 113 ));
            _worldFlags.Add((185 , "The Gambia - Banjul" , 0 , 2 , 64 ));
            _worldFlags.Add((188 , "Togo - Lome" , 0 , 1 , 130 ));
            _worldFlags.Add((191 , "Tunisia - Tunis" , 0 , 2 , 32 ));
            _worldFlags.Add((195 , "Uganda - Kampala" , 0 , 2 , 113 ));
            _worldFlags.Add((206 , "Western Sahara - El Aaiún (disputed)" , 0 , 2 , 121 ));
            _worldFlags.Add((208 , "Zambia - Lusaka" , 0 , 2 , 123 ));
            _worldFlags.Add((3 , "Afghanistan - Kabul" , 1 , 1 , 4 ));
            _worldFlags.Add((13 , "Armenia - Yerevan" , 1 , 1 , 13 ));
            _worldFlags.Add((16 , "Azerbaijan - Baku" , 1 , 1 , 10 ));
            _worldFlags.Add((18 , "Bahrain - Manama" , 1 , 2 , 15 ));
            _worldFlags.Add((19 , "Bangladesh (বাংলাদেশ) - Dhaka (ঢাকা)" , 1 , 2 , 16 ));
            _worldFlags.Add((21 , "Bashkortstan" , 1 , 1 , 17 ));
            _worldFlags.Add((27 , "Bhutan - Thimphu" , 1 , 1 , 15 ));
            _worldFlags.Add((32 , "Brunei - Bandar Seri Begawan" , 1 , 2 , 26 ));
            _worldFlags.Add((36 , "Buryatia" , 1 , 1 , 18 ));
            _worldFlags.Add((37 , "Cambodia (Kampuchea) - Phnom Penh" , 1 , 2 , 29 ));
            _worldFlags.Add((45 , "China - Beijing" , 1 , 1 , 36 ));
            _worldFlags.Add((60 , "East Timor (Timor Leste) - Dili" , 1 , 2 , 47 ));
            _worldFlags.Add((75 , "Georgia - Tbilisi" , 1 , 1 , 61 ));
            _worldFlags.Add((88 , "Hong Kong - Hong Kong" , 1 , 1 , 70 ));
            _worldFlags.Add((91 , "India - New Delhi" , 1 , 1 , 72 ));
            _worldFlags.Add((92 , "Indonesia - Jakarta" , 1 , 1 , 77 ));
            _worldFlags.Add((93 , "Iran - Tehran" , 1 , 1 , 76 ));
            _worldFlags.Add((94 , "Iraq - Baghdad" , 1 , 1 , 73 ));
            _worldFlags.Add((95 , "Ireland - Dublin" , 1 , 1 , 79 ));
            _worldFlags.Add((96 , "Israel - Jerusalem" , 1 , 2 , 74 ));
            _worldFlags.Add((99 , "Japan - Tokyo" , 1 , 2 , 78 ));
            _worldFlags.Add((100 , "Jordan (Al Urdun) - Amman" , 1 , 2 , 79 ));
            _worldFlags.Add((101 , "Kazakhstan - Nur-Sultan" , 1 , 2 , 89 ));
            _worldFlags.Add((105 , "Kuwait - Kuwait City" , 1 , 2 , 85 ));
            _worldFlags.Add((106 , "Kyrgyzstan - Bishkek" , 1 , 1 , 89 ));
            _worldFlags.Add((107 , "Laos - Vientiane" , 1 , 1 , 90 ));
            _worldFlags.Add((109 , "Lebanon (Lubnan) - Beirut" , 1 , 1 , 93 ));
            _worldFlags.Add((118 , "Malaysia - Kuala Lumpur" , 1 , 2 , 97 ));
            _worldFlags.Add((119 , "Maldives" , 1 , 1 , 96 ));
            _worldFlags.Add((128 , "Mongolia - Ulaanbaatar" , 1 , 1 , 109 ));
            _worldFlags.Add((132 , "Myanmar (Burma) - Naypyidaw" , 1 , 1 , 111 ));
            _worldFlags.Add((134 , "Nepal - Kathmandu" , 1 , 1 , 117 ));
            _worldFlags.Add((140 , "North Korea - Pyongyang" , 1 , 1 , 86 ));
            _worldFlags.Add((142 , "Oman - Muscat" , 1 , 1 , 121 ));
            _worldFlags.Add((143 , "Pakistan - Islamabad" , 1 , 1 , 127 ));
            _worldFlags.Add((145 , "Palestine - Ramallah" , 1 , 1 , 122 ));
            _worldFlags.Add((152 , "Qatar - Doha" , 1 , 2 , 127 ));
            _worldFlags.Add((161 , "Saudi Arabia - Riyadh" , 1 , 2 , 139 ));
            _worldFlags.Add((166 , "Singapore - Singapore" , 1 , 2 , 61 ));
            _worldFlags.Add((173 , "South Korea - Seoul" , 1 , 1 , 88 ));
            _worldFlags.Add((175 , "Sri Lanka - Sri Jayawardenapura Kotte" , 1 , 2 , 20 ));
            _worldFlags.Add((180 , "Syria - Damascus" , 1 , 2 , 73 ));
            _worldFlags.Add((181 , "Taiwan - Taipei" , 1 , 2 , 23 ));
            _worldFlags.Add((182 , "Tajikistan - Dushanbe" , 1 , 2 , 102 ));
            _worldFlags.Add((184 , "Thailand (Muang Thai) - Bangkok" , 1 , 2 , 22 ));
            _worldFlags.Add((186 , "The Maldives - Malé" , 1 , 2 , 96 ));
            _worldFlags.Add((187 , "The Philippines - Manila" , 1 , 2 , 124 ));
            _worldFlags.Add((192 , "Turkey (Türkiye) - Ankara" , 1 , 2 , 83 ));
            _worldFlags.Add((193 , "Turkmenistan - Aşgabat" , 1 , 1 , 131 ));
            _worldFlags.Add((196 , "United Arab Emirates - Abu Dhabi" , 1 , 2 , 111 ));
            _worldFlags.Add((201 , "Uzbekistan - Tashkent" , 1 , 2 , 91 ));
            _worldFlags.Add((205 , "Vietnam - Hanoi" , 1 , 2 , 43 ));
            _worldFlags.Add((207 , "Yemen - Sana'a" , 1 , 2 , 122 ));
            _worldFlags.Add((4 , "Albania (Shqipëria) - Tirana" , 2 , 1 , 3 ));
            _worldFlags.Add((9 , "Andorra - Andorra la Vella" , 2 , 1 , 8 ));
            _worldFlags.Add((15 , "Austria (Österreich) - Vienna" , 2 , 1 , 14 ));
            _worldFlags.Add((22 , "Belarus (Беларусь) - Minsk" , 2 , 1 , 28 ));
            _worldFlags.Add((23 , "Belgium  - Brussels" , 2 , 1 , 19 ));
            _worldFlags.Add((29 , "Bosnia and Herzegovina  - Sarajevo" , 2 , 1 , 26 ));
            _worldFlags.Add((33 , "Bulgaria (България) - Sofia" , 2 , 2 , 25 ));
            _worldFlags.Add((51 , "Croatia (Hrvatska) - Zagreb" , 2 , 2 , 39 ));
            _worldFlags.Add((53 , "Cyprus (Κύπρος) - Nicosia" , 2 , 1 , 35 ));
            _worldFlags.Add((54 , "Czech Republic (Česko) - Prague" , 2 , 1 , 34 ));
            _worldFlags.Add((56 , "Denmark (Danmark) - Copenhagen" , 2 , 1 , 48 ));
            _worldFlags.Add((64 , "England - London" , 2 , 1 , 54 ));
            _worldFlags.Add((67 , "Estonia (Eesti) - Tallinn" , 2 , 2 , 49 ));
            _worldFlags.Add((72 , "Finland (Suomi) - Helsinki" , 2 , 1 , 58 ));
            _worldFlags.Add((73 , "France - Paris" , 2 , 2 , 56 ));
            _worldFlags.Add((76 , "Germany (Deutschland) - Berlin" , 2 , 1 , 64 ));
            _worldFlags.Add((78 , "Gilbraltar" , 2 , 1 , 69 ));
            _worldFlags.Add((79 , "Greece (Ελλάδα) - Athens" , 2 , 1 , 63 ));
            _worldFlags.Add((89 , "Hungary (Magyarország) - Budapest" , 2 , 2 , 68 ));
            _worldFlags.Add((90 , "Iceland - Reykjavik" , 2 , 1 , 75 ));
            _worldFlags.Add((97 , "Italy (Italia) - Rome" , 2 , 1 , 78 ));
            _worldFlags.Add((104 , "Kosovo - Pristina" , 2 , 2 , 88 ));
            _worldFlags.Add((108 , "Latvia (Latvija) - Riga" , 2 , 1 , 97 ));
            _worldFlags.Add((113 , "Liechtenstein - Vaduz" , 2 , 1 , 92 ));
            _worldFlags.Add((114 , "Lithuania (Lietuva) - Vilnius" , 2 , 1 , 95 ));
            _worldFlags.Add((115 , "Luxembourg - Luxembourg City" , 2 , 2 , 94 ));
            _worldFlags.Add((116 , "Macedonia" , 2 , 1 , 105 ));
            _worldFlags.Add((121 , "Malta - Valletta" , 2 , 2 , 95 ));
            _worldFlags.Add((126 , "Moldova - Chisinau" , 2 , 2 , 108 ));
            _worldFlags.Add((127 , "Monaco - Monte Carlo Quarter" , 2 , 1 , 107 ));
            _worldFlags.Add((129 , "Montenegro (Crna Gora, Црна Гора) - Podgorica" , 2 , 2 , 105 ));
            _worldFlags.Add((135 , "Netherlands - Amsterdam" , 2 , 1 , 118 ));
            _worldFlags.Add((141 , "Norway (Norge) - Oslo" , 2 , 2 , 116 ));
            _worldFlags.Add((149 , "Poland (Polska) - Warsaw" , 2 , 1 , 126 ));
            _worldFlags.Add((150 , "Portugal - Lisbon" , 2 , 1 , 124 ));
            _worldFlags.Add((153 , "Republic of Ireland (Éire) - Dublin" , 2 , 1 , 79 ));
            _worldFlags.Add((155 , "Romania - Bucharest" , 2 , 2 , 132 ));
            _worldFlags.Add((156 , "Russia - Moscow" , 2 , 2 , 129 ));
            _worldFlags.Add((159 , "San Marino - San Marino" , 2 , 2 , 138 ));
            _worldFlags.Add((163 , "Serbia (Србија) - Belgrade" , 2 , 1 , 132 ));
            _worldFlags.Add((167 , "Slovakia (Slovensko) - Bratislava" , 2 , 2 , 62 ));
            _worldFlags.Add((168 , "Slovenia (Slovenija) - Ljubljana" , 2 , 1 , 134 ));
            _worldFlags.Add((174 , "Spain (España) - Madrid" , 2 , 1 , 135 ));
            _worldFlags.Add((178 , "Sweden (Sverige) - Stockholm" , 2 , 2 , 21 ));
            _worldFlags.Add((179 , "Switzerland - Bern" , 2 , 2 , 70 ));
            _worldFlags.Add((197 , "United Kingdom - London" , 2 , 1 , 54 ));
            _worldFlags.Add((203 , "Vatican City - Vatican City" , 2 , 2 , 93 ));
            _worldFlags.Add((11 , "Aruba" , 3 , 1 , 11 ));
            _worldFlags.Add((17 , "Bahamas - Nassau" , 3 , 2 , 14 ));
            _worldFlags.Add((20 , "Barbados - Bridgetown" , 3 , 2 , 17 ));
            _worldFlags.Add((24 , "Belize - Belmopan" , 3 , 1 , 16 ));
            _worldFlags.Add((26 , "Bermuda" , 3 , 1 , 20 ));
            _worldFlags.Add((40 , "Canada - Ottawa" , 3 , 1 , 37 ));
            _worldFlags.Add((39 , "Cayman Islands" , 3 , 1 , 38 ));
            _worldFlags.Add((49 , "Costa Rica - San José" , 3 , 2 , 36 ));
            _worldFlags.Add((52 , "Cuba - Havana" , 3 , 2 , 35 ));
            _worldFlags.Add((58 , "Dominica - Roseau" , 3 , 1 , 49 ));
            _worldFlags.Add((59 , "Dominican Republic - Santo Domingo" , 3 , 1 , 47 ));
            _worldFlags.Add((63 , "El Salvador - San Salvador" , 3 , 1 , 52 ));
            _worldFlags.Add((80 , "Greenland (territory of Denmark)" , 3 , 2 , 59 ));
            _worldFlags.Add((81 , "Grenada" , 3 , 1 , 58 ));
            _worldFlags.Add((82 , "Guatemala - Guatemala City" , 3 , 1 , 62 ));
            _worldFlags.Add((86 , "Haiti - Port-au-Prince" , 3 , 1 , 71 ));
            _worldFlags.Add((87 , "Honduras - Tegucigalpa" , 3 , 2 , 67 ));
            _worldFlags.Add((98 , "Jamaica - Kingston" , 3 , 2 , 77 ));
            _worldFlags.Add((125 , "Mexico - Mexico City" , 3 , 2 , 98 ));
            _worldFlags.Add((137 , "Nicaragua - Managua" , 3 , 2 , 115 ));
            _worldFlags.Add((151 , "Puerto Rico - San Juan (territory of U.S.)" , 3 , 2 , 125 ));
            _worldFlags.Add((198 , "United States of America - Washington, District of Columbia" , 3 , 2 , 130 ));
            _worldFlags.Add((200 , "US Virgin Islands - Charlotte Amalie (territory of U.S.)" , 3 , 2 , 43 ));
            _worldFlags.Add((14 , "Australia - Canberra" , 4 , 2 , 5 ));
            _worldFlags.Add((70 , "Federated States of Micronesia - Palikir" , 4 , 2 , 106 ));
            _worldFlags.Add((71 , "Fiji - Suva" , 4 , 2 , 55 ));
            _worldFlags.Add((103 , "Kiribati - South Tarawa" , 4 , 1 , 87 ));
            _worldFlags.Add((122 , "Marshall Islands - Majuro" , 4 , 1 , 103 ));
            _worldFlags.Add((136 , "New Zealand - Wellington" , 4 , 1 , 114 ));
            _worldFlags.Add((144 , "Palau - Ngerulmud" , 4 , 1 , 129 ));
            _worldFlags.Add((146 , "Papua New Guinea - Port Moresby" , 4 , 1 , 123 ));
            _worldFlags.Add((158 , "Samoa - Apia" , 4 , 2 , 52 ));
            _worldFlags.Add((169 , "Solomon Islands - Honiara" , 4 , 1 , 137 ));
            _worldFlags.Add((189 , "Tonga - Nuku'alofa" , 4 , 2 , 101 ));
            _worldFlags.Add((194 , "Tuvalu - Funafuti" , 4 , 2 , 110 ));
            _worldFlags.Add((202 , "Vanuatu - Port Vila" , 4 , 2 , 92 ));
            _worldFlags.Add((12 , "Argentina - Buenos Aires" , 5 , 1 , 5 ));
            _worldFlags.Add((28 , "Bolivia - Sucré" , 5 , 1 , 25 ));
            _worldFlags.Add((31 , "Brazil (Brasil) - Brasília" , 5 , 1 , 21 ));
            _worldFlags.Add((44 , "Chile - Santiago" , 5 , 1 , 30 ));
            _worldFlags.Add((46 , "Colombia - Bogotá" , 5 , 1 , 43 ));
            _worldFlags.Add((61 , "Ecuador - Quito" , 5 , 2 , 48 ));
            _worldFlags.Add((69 , "Falkland Islands - Stanley (territory of U.K.)" , 5 , 2 , 46 ));
            _worldFlags.Add((85 , "Guyana - Georgetown" , 5 , 2 , 65 ));
            _worldFlags.Add((147 , "Paraguay - Asunción" , 5 , 1 , 128 ));
            _worldFlags.Add((148 , "Peru - Lima" , 5 , 1 , 125 ));
            _worldFlags.Add((172 , "South Georgia and the South Sandwich Islands - (territory of U.K.)" , 5 , 1 , 139 ));
            _worldFlags.Add((176 , "Suriname - Paramaribo" , 5 , 2 , 71 ));
            _worldFlags.Add((190 , "Trinidad and Tobago - Port of Spain" , 5 , 2 , 81 ));
            _worldFlags.Add((199 , "Uruguay - Montevideo" , 5 , 2 , 90 ));
            _worldFlags.Add((204 , "Venezuela - Caracas" , 5 , 2 , 40 ));
            #endregion
            
        }
    }
    

}

