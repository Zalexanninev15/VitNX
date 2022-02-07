using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace VitNX.UI.OtherControls
{
    public class FontAwesome4
    {
        public class Properties
        {
            public int Size { get; set; }
            public Point Location { get; set; }
            public Color ForeColor { get; set; }
            public Color BackColor { get; set; }
            public Color BorderColor { get; set; }
            public bool ShowBorder { get; set; }
            public Type Type { get; set; }

            private static Properties _default;

            private Properties()
            {
                Size = 32;
                Location = new Point(0, 0);
                ForeColor = Color.Black;
                BackColor = Color.Transparent;
                BorderColor = Color.Gray;
                ShowBorder = false;
            }

            public Properties(Type type)
            {
                Size = Default.Size;
                Location = Default.Location;
                ForeColor = Default.ForeColor;
                BackColor = Default.BackColor;
                BorderColor = Default.BorderColor;
                ShowBorder = Default.ShowBorder;
                Type = type;
            }

            public static Properties Get(Type type = Type.None)
            {
                var props = Default;
                props.Type = type;
                return props;
            }

            public static Properties Default
            {
                get
                {
                    if (_default == null)
                        _default = new Properties();
                    return _default;
                }
                internal set { _default = value; }
            }
        }

        private PrivateFontCollection _fonts = new PrivateFontCollection();
        private const string FONT_FILE_NAME = "fontawesome-webfont.ttf";

        private static FontAwesome4 _instance;

        public static void Initialize()
        {
            if (_instance == null)
                _instance = new FontAwesome4();
        }

        public static FontAwesome4 Instance
        {
            get
            {
                if (_instance == null)
                    Initialize();
                return _instance;
            }
        }

        private static string _downloadLink = "https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/fonts/fontawesome-webfont.ttf?v=4.7.0";

        public static void SetDownloadLink(string link)
        {
            _downloadLink = link;
        }

        public static Properties DefaultProperties
        {
            get { return Properties.Default; }
        }

        public void SetDefaultProperties(Properties props)
        {
            Properties.Default = props;
        }

        public Icon GetIcon(Type type, Properties props = null)
        {
            if (props == null)
                props = Properties.Default;
            props.Type = type;
            return GetIcon(props);
        }

        public Icon GetIcon(Properties props)
        {
            var img = GetImage(props);
            return Icon.FromHandle(img.GetHicon());
        }

        public Bitmap GetImage(string name, Properties props = null)
        {
            if (props == null)
                props = Properties.Default;
            if (props.Type != Type.None)
                return GetImage(Properties.Get(ParseType(name)));
            return null;
        }

        public Bitmap GetImage(Type type, Properties props = null)
        {
            if (props == null)
                props = Properties.Default;
            props.Type = type;
            return GetImage(props);
        }

        public Bitmap GetImage(Properties props)
        {
            return GetImageInternal(props);
        }

        private FontAwesome4()
        {
            LoadFont();
        }

        private Bitmap GetImageInternal(Properties props)
        {
            var size = GetFontIconRealSize(props.Size, (int)props.Type);
            var bmpTemp = new Bitmap(size.Width, size.Height);
            using (Graphics g1 = Graphics.FromImage(bmpTemp))
            {
                g1.TextRenderingHint = TextRenderingHint.AntiAlias;
                g1.Clear(Color.Transparent);
                var font = GetIconFont(props.Size);
                string character = char.ConvertFromUtf32((int)props.Type);
                var format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.Character
                };
                g1.DrawString(character, font, new SolidBrush(props.ForeColor), 0, 0);
                g1.DrawImage(bmpTemp, 0, 0);
            }
            var bmp = ResizeImage(bmpTemp, props);
            if (props.ShowBorder)
            {
                using (Graphics g2 = Graphics.FromImage(bmp))
                {
                    var pen = new Pen(props.BorderColor, 1);
                    var borderRect = new Rectangle(0, 0, (int)(props.Size - pen.Width), (int)(props.Size - pen.Width));
                    g2.DrawRectangle(pen, borderRect);
                }
            }
            return bmp;
        }

        private Size GetFontIconRealSize(int size, int iconIndex)
        {
            var bmpTemp = new Bitmap(size, size);
            using (Graphics g1 = Graphics.FromImage(bmpTemp))
            {
                g1.TextRenderingHint = TextRenderingHint.AntiAlias;
                g1.PixelOffsetMode = PixelOffsetMode.HighQuality;
                var font = GetIconFont(size);
                string character = char.ConvertFromUtf32(iconIndex);
                var format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.Word
                };
                var sizeF = g1.MeasureString(character, font, new Point(0, 0), format);
                return sizeF.ToSize();
            }
        }

        private Bitmap ResizeImage(Bitmap imgToResize, Properties props)
        {
            var srcWidth = imgToResize.Width;
            var srcHeight = imgToResize.Height;
            float ratio = (srcWidth > srcHeight) ? (srcWidth / (float)srcHeight) : (srcHeight / (float)srcWidth);
            var dstWidth = (int)Math.Ceiling(srcWidth / ratio);
            var dstHeight = (int)Math.Ceiling(srcHeight / ratio);
            var x = (int)Math.Round((props.Size - dstWidth) / 2f, 0);
            var y = (int)(1 + Math.Round((props.Size - dstHeight) / 2f, 0));
            Bitmap b = new Bitmap(props.Size + props.Location.X, props.Size + props.Location.Y);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(props.BackColor);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(imgToResize, x + props.Location.X, y + props.Location.Y, dstWidth, dstHeight);
            }
            return b;
        }

        private void LoadFont()
        {
            try
            {
                if (!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{FONT_FILE_NAME}") && !string.IsNullOrEmpty(_downloadLink))
                {
                    Uri downloadUri;
                    if (Uri.TryCreate(_downloadLink, UriKind.Absolute, out downloadUri)
                        && (downloadUri.Scheme == Uri.UriSchemeHttp || downloadUri.Scheme == Uri.UriSchemeHttps))
                    {
                        using (var client = new System.Net.WebClient())
                            client.DownloadFile(downloadUri, $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{FONT_FILE_NAME}");
                    }
                }
            }
            finally
            {
                if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{FONT_FILE_NAME}"))
                    _fonts.AddFontFile($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{FONT_FILE_NAME}");
            }
        }

        private Font GetIconFont(int pixelSize)
        {
            var size = pixelSize / (16f / 12f);
            var font = new Font(_fonts.Families[0], size, FontStyle.Regular, GraphicsUnit.Point);
            return font;
        }

        public static Type ParseType(string name)
        {
            Type retval = Type.Empty;
            if (!string.IsNullOrEmpty(name))
                retval = (Type)Enum.Parse(typeof(Type), name);
            return retval;
        }

        public enum Type
        {
            None = 0x0,
            Empty = 0x0,
            Px500 = 0xf26e,
            AddressBook = 0xf2B9,
            AddressBookO = 0xf2Ba,
            AddressCard = 0xf2Bb,
            AddressCardO = 0xf2Bc,
            Adjust = 0xf042,
            Adn = 0xf170,
            AlignCenter = 0xf037,
            AlignJustify = 0xf039,
            AlignLeft = 0xf036,
            AlignRight = 0xf038,
            Amazon = 0xf270,
            Ambulance = 0xf0F9,
            AmericanSignLanguageInterpreting = 0xf2A3,
            Anchor = 0xf13D,
            Android = 0xf17B,
            Angellist = 0xf209,
            AngleDoubleDown = 0xf103,
            AngleDoubleLeft = 0xf100,
            AngleDoubleRight = 0xf101,
            AngleDoubleUp = 0xf102,
            AngleDown = 0xf107,
            AngleLeft = 0xf104,
            AngleRight = 0xf105,
            AngleUp = 0xf106,
            Apple = 0xf179,
            Archive = 0xf187,
            AreaChart = 0xf1Fe,
            ArrowCircleDown = 0xf0Ab,
            ArrowCircleLeft = 0xf0A8,
            ArrowCircleODown = 0xf01A,
            ArrowCircleOLeft = 0xf190,
            ArrowCircleORight = 0xf18E,
            ArrowCircleOUp = 0xf01B,
            ArrowCircleRight = 0xf0A9,
            ArrowCircleUp = 0xf0Aa,
            ArrowDown = 0xf063,
            ArrowLeft = 0xf060,
            ArrowRight = 0xf061,
            ArrowUp = 0xf062,
            Arrows = 0xf047,
            ArrowsAlt = 0xf0B2,
            ArrowsH = 0xf07E,
            ArrowsV = 0xf07D,
            AslInterpreting = 0xf2A3,
            AssistiveListeningSystems = 0xf2A2,
            Asterisk = 0xf069,
            At = 0xf1Fa,
            AudioDescription = 0xf29E,
            Automobile = 0xf1B9,
            Backward = 0xf04A,
            BalanceScale = 0xf24E,
            Ban = 0xf05E,
            Bandcamp = 0xf2D5,
            Bank = 0xf19C,
            BarChart = 0xf080,
            BarChartO = 0xf080,
            Barcode = 0xf02A,
            Bars = 0xf0C9,
            Bath = 0xf2Cd,
            Bathtub = 0xf2Cd,
            Battery = 0xf240,
            Battery0 = 0xf244,
            Battery1 = 0xf243,
            Battery2 = 0xf242,
            Battery3 = 0xf241,
            Battery4 = 0xf240,
            BatteryEmpty = 0xf244,
            BatteryFull = 0xf240,
            BatteryHalf = 0xf242,
            BatteryQuarter = 0xf243,
            BatteryThreeQuarters = 0xf241,
            Bed = 0xf236,
            Beer = 0xf0Fc,
            Behance = 0xf1B4,
            BehanceSquare = 0xf1B5,
            Bell = 0xf0F3,
            BellO = 0xf0A2,
            BellSlash = 0xf1F6,
            BellSlashO = 0xf1F7,
            Bicycle = 0xf206,
            Binoculars = 0xf1E5,
            BirthdayCake = 0xf1Fd,
            Bitbucket = 0xf171,
            BitbucketSquare = 0xf172,
            Bitcoin = 0xf15A,
            BlackTie = 0xf27E,
            Blind = 0xf29D,
            Bluetooth = 0xf293,
            BluetoothB = 0xf294,
            Bold = 0xf032,
            Bolt = 0xf0E7,
            Bomb = 0xf1E2,
            Book = 0xf02D,
            Bookmark = 0xf02E,
            BookmarkO = 0xf097,
            Braille = 0xf2A1,
            Briefcase = 0xf0B1,
            Btc = 0xf15A,
            Bug = 0xf188,
            Building = 0xf1Ad,
            BuildingO = 0xf0F7,
            Bullhorn = 0xf0A1,
            Bullseye = 0xf140,
            Bus = 0xf207,
            Buysellads = 0xf20D,
            Cab = 0xf1Ba,
            Calculator = 0xf1Ec,
            Calendar = 0xf073,
            CalendarCheckO = 0xf274,
            CalendarMinusO = 0xf272,
            CalendarO = 0xf133,
            CalendarPlusO = 0xf271,
            CalendarTimesO = 0xf273,
            Camera = 0xf030,
            CameraRetro = 0xf083,
            Car = 0xf1B9,
            CaretDown = 0xf0D7,
            CaretLeft = 0xf0D9,
            CaretRight = 0xf0Da,
            CaretSquareODown = 0xf150,
            CaretSquareOLeft = 0xf191,
            CaretSquareORight = 0xf152,
            CaretSquareOUp = 0xf151,
            CaretUp = 0xf0D8,
            CartArrowDown = 0xf218,
            CartPlus = 0xf217,
            Cc = 0xf20A,
            CcAmex = 0xf1F3,
            CcDinersClub = 0xf24C,
            CcDiscover = 0xf1F2,
            CcJcb = 0xf24B,
            CcMastercard = 0xf1F1,
            CcPaypal = 0xf1F4,
            CcStripe = 0xf1F5,
            CcVisa = 0xf1F0,
            Certificate = 0xf0A3,
            Chain = 0xf0C1,
            ChainBroken = 0xf127,
            Check = 0xf00C,
            CheckCircle = 0xf058,
            CheckCircleO = 0xf05D,
            CheckSquare = 0xf14A,
            CheckSquareO = 0xf046,
            ChevronCircleDown = 0xf13A,
            ChevronCircleLeft = 0xf137,
            ChevronCircleRight = 0xf138,
            ChevronCircleUp = 0xf139,
            ChevronDown = 0xf078,
            ChevronLeft = 0xf053,
            ChevronRight = 0xf054,
            ChevronUp = 0xf077,
            Child = 0xf1Ae,
            Chrome = 0xf268,
            Circle = 0xf111,
            CircleO = 0xf10C,
            CircleONotch = 0xf1Ce,
            CircleThin = 0xf1Db,
            Clipboard = 0xf0Ea,
            ClockO = 0xf017,
            Clone = 0xf24D,
            Close = 0xf00D,
            Cloud = 0xf0C2,
            CloudDownload = 0xf0Ed,
            CloudUpload = 0xf0Ee,
            Cny = 0xf157,
            Code = 0xf121,
            CodeFork = 0xf126,
            Codepen = 0xf1Cb,
            Codiepie = 0xf284,
            Coffee = 0xf0F4,
            Cog = 0xf013,
            Cogs = 0xf085,
            Columns = 0xf0Db,
            Comment = 0xf075,
            CommentO = 0xf0E5,
            Commenting = 0xf27A,
            CommentingO = 0xf27B,
            Comments = 0xf086,
            CommentsO = 0xf0E6,
            Compass = 0xf14E,
            Compress = 0xf066,
            Connectdevelop = 0xf20E,
            Contao = 0xf26D,
            Copy = 0xf0C5,
            Copyright = 0xf1F9,
            CreativeCommons = 0xf25E,
            CreditCard = 0xf09D,
            CreditCardAlt = 0xf283,
            Crop = 0xf125,
            Crosshairs = 0xf05B,
            Css3 = 0xf13C,
            Cube = 0xf1B2,
            Cubes = 0xf1B3,
            Cut = 0xf0C4,
            Cutlery = 0xf0F5,
            Dashboard = 0xf0E4,
            Dashcube = 0xf210,
            Database = 0xf1C0,
            Deaf = 0xf2A4,
            Deafness = 0xf2A4,
            Dedent = 0xf03B,
            Delicious = 0xf1A5,
            Desktop = 0xf108,
            Deviantart = 0xf1Bd,
            Diamond = 0xf219,
            Digg = 0xf1A6,
            Dollar = 0xf155,
            DotCircleO = 0xf192,
            Download = 0xf019,
            Dribbble = 0xf17D,
            DriversLicense = 0xf2C2,
            DriversLicenseO = 0xf2C3,
            Dropbox = 0xf16B,
            Drupal = 0xf1A9,
            Edge = 0xf282,
            Edit = 0xf044,
            Eercast = 0xf2Da,
            Eject = 0xf052,
            EllipsisH = 0xf141,
            EllipsisV = 0xf142,
            Empire = 0xf1D1,
            Envelope = 0xf0E0,
            EnvelopeO = 0xf003,
            EnvelopeOpen = 0xf2B6,
            EnvelopeOpenO = 0xf2B7,
            EnvelopeSquare = 0xf199,
            Envira = 0xf299,
            Eraser = 0xf12D,
            Etsy = 0xf2D7,
            Eur = 0xf153,
            Euro = 0xf153,
            Exchange = 0xf0Ec,
            Exclamation = 0xf12A,
            ExclamationCircle = 0xf06A,
            ExclamationTriangle = 0xf071,
            Expand = 0xf065,
            Expeditedssl = 0xf23E,
            ExternalLink = 0xf08E,
            ExternalLinkSquare = 0xf14C,
            Eye = 0xf06E,
            EyeSlash = 0xf070,
            Eyedropper = 0xf1Fb,
            Fa = 0xf2B4,
            Facebook = 0xf09A,
            FacebookF = 0xf09A,
            FacebookOfficial = 0xf230,
            FacebookSquare = 0xf082,
            FastBackward = 0xf049,
            FastForward = 0xf050,
            Fax = 0xf1Ac,
            Feed = 0xf09E,
            Female = 0xf182,
            FighterJet = 0xf0Fb,
            File = 0xf15B,
            FileArchiveO = 0xf1C6,
            FileAudioO = 0xf1C7,
            FileCodeO = 0xf1C9,
            FileExcelO = 0xf1C3,
            FileImageO = 0xf1C5,
            FileMovieO = 0xf1C8,
            FileO = 0xf016,
            FilePdfO = 0xf1C1,
            FilePhotoO = 0xf1C5,
            FilePictureO = 0xf1C5,
            FilePowerpointO = 0xf1C4,
            FileSoundO = 0xf1C7,
            FileText = 0xf15C,
            FileTextO = 0xf0F6,
            FileVideoO = 0xf1C8,
            FileWordO = 0xf1C2,
            FileZipO = 0xf1C6,
            FilesO = 0xf0C5,
            Film = 0xf008,
            Filter = 0xf0B0,
            Fire = 0xf06D,
            FireExtinguisher = 0xf134,
            Firefox = 0xf269,
            FirstOrder = 0xf2B0,
            Flag = 0xf024,
            FlagCheckered = 0xf11E,
            FlagO = 0xf11D,
            Flash = 0xf0E7,
            Flask = 0xf0C3,
            Flickr = 0xf16E,
            FloppyO = 0xf0C7,
            Folder = 0xf07B,
            FolderO = 0xf114,
            FolderOpen = 0xf07C,
            FolderOpenO = 0xf115,
            Font = 0xf031,
            FontAwesome = 0xf2B4,
            Fonticons = 0xf280,
            FortAwesome = 0xf286,
            Forumbee = 0xf211,
            Forward = 0xf04E,
            Foursquare = 0xf180,
            FreeCodeCamp = 0xf2C5,
            FrownO = 0xf119,
            FutbolO = 0xf1E3,
            Gamepad = 0xf11B,
            Gavel = 0xf0E3,
            Gbp = 0xf154,
            Ge = 0xf1D1,
            Gear = 0xf013,
            Gears = 0xf085,
            Genderless = 0xf22D,
            GetPocket = 0xf265,
            Gg = 0xf260,
            GgCircle = 0xf261,
            Gift = 0xf06B,
            Git = 0xf1D3,
            GitSquare = 0xf1D2,
            Github = 0xf09B,
            GithubAlt = 0xf113,
            GithubSquare = 0xf092,
            Gitlab = 0xf296,
            Gittip = 0xf184,
            Glass = 0xf000,
            Glide = 0xf2A5,
            GlideG = 0xf2A6,
            Globe = 0xf0Ac,
            Google = 0xf1A0,
            GooglePlus = 0xf0D5,
            GooglePlusCircle = 0xf2B3,
            GooglePlusOfficial = 0xf2B3,
            GooglePlusSquare = 0xf0D4,
            GoogleWallet = 0xf1Ee,
            GraduationCap = 0xf19D,
            Gratipay = 0xf184,
            Grav = 0xf2D6,
            Group = 0xf0C0,
            HSquare = 0xf0Fd,
            HackerNews = 0xf1D4,
            HandGrabO = 0xf255,
            HandLizardO = 0xf258,
            HandODown = 0xf0A7,
            HandOLeft = 0xf0A5,
            HandORight = 0xf0A4,
            HandOUp = 0xf0A6,
            HandPaperO = 0xf256,
            HandPeaceO = 0xf25B,
            HandPointerO = 0xf25A,
            HandRockO = 0xf255,
            HandScissorsO = 0xf257,
            HandSpockO = 0xf259,
            HandStopO = 0xf256,
            HandshakeO = 0xf2B5,
            HardOfHearing = 0xf2A4,
            Hashtag = 0xf292,
            HddO = 0xf0A0,
            Header = 0xf1Dc,
            Headphones = 0xf025,
            Heart = 0xf004,
            HeartO = 0xf08A,
            Heartbeat = 0xf21E,
            History = 0xf1Da,
            Home = 0xf015,
            HospitalO = 0xf0F8,
            Hotel = 0xf236,
            Hourglass = 0xf254,
            Hourglass1 = 0xf251,
            Hourglass2 = 0xf252,
            Hourglass3 = 0xf253,
            HourglassEnd = 0xf253,
            HourglassHalf = 0xf252,
            HourglassO = 0xf250,
            HourglassStart = 0xf251,
            Houzz = 0xf27C,
            Html5 = 0xf13B,
            ICursor = 0xf246,
            IdBadge = 0xf2C1,
            IdCard = 0xf2C2,
            IdCardO = 0xf2C3,
            Ils = 0xf20B,
            Image = 0xf03E,
            Imdb = 0xf2D8,
            Inbox = 0xf01C,
            Indent = 0xf03C,
            Industry = 0xf275,
            Info = 0xf129,
            InfoCircle = 0xf05A,
            Inr = 0xf156,
            Instagram = 0xf16D,
            Institution = 0xf19C,
            InternetExplorer = 0xf26B,
            Intersex = 0xf224,
            Ioxhost = 0xf208,
            Italic = 0xf033,
            Joomla = 0xf1Aa,
            Jpy = 0xf157,
            Jsfiddle = 0xf1Cc,
            Key = 0xf084,
            KeyboardO = 0xf11C,
            Krw = 0xf159,
            Language = 0xf1Ab,
            Laptop = 0xf109,
            Lastfm = 0xf202,
            LastfmSquare = 0xf203,
            Leaf = 0xf06C,
            Leanpub = 0xf212,
            Legal = 0xf0E3,
            LemonO = 0xf094,
            LevelDown = 0xf149,
            LevelUp = 0xf148,
            LifeBouy = 0xf1Cd,
            LifeBuoy = 0xf1Cd,
            LifeRing = 0xf1Cd,
            LifeSaver = 0xf1Cd,
            LightbulbO = 0xf0Eb,
            LineChart = 0xf201,
            Link = 0xf0C1,
            Linkedin = 0xf0E1,
            LinkedinSquare = 0xf08C,
            Linode = 0xf2B8,
            Linux = 0xf17C,
            List = 0xf03A,
            ListAlt = 0xf022,
            ListOl = 0xf0Cb,
            ListUl = 0xf0Ca,
            LocationArrow = 0xf124,
            Lock = 0xf023,
            LongArrowDown = 0xf175,
            LongArrowLeft = 0xf177,
            LongArrowRight = 0xf178,
            LongArrowUp = 0xf176,
            LowVision = 0xf2A8,
            Magic = 0xf0D0,
            Magnet = 0xf076,
            MailForward = 0xf064,
            MailReply = 0xf112,
            MailReplyAll = 0xf122,
            Male = 0xf183,
            Map = 0xf279,
            MapMarker = 0xf041,
            MapO = 0xf278,
            MapPin = 0xf276,
            MapSigns = 0xf277,
            Mars = 0xf222,
            MarsDouble = 0xf227,
            MarsStroke = 0xf229,
            MarsStrokeH = 0xf22B,
            MarsStrokeV = 0xf22A,
            Maxcdn = 0xf136,
            Meanpath = 0xf20C,
            Medium = 0xf23A,
            Medkit = 0xf0Fa,
            Meetup = 0xf2E0,
            MehO = 0xf11A,
            Mercury = 0xf223,
            Microchip = 0xf2Db,
            Microphone = 0xf130,
            MicrophoneSlash = 0xf131,
            Minus = 0xf068,
            MinusCircle = 0xf056,
            MinusSquare = 0xf146,
            MinusSquareO = 0xf147,
            Mixcloud = 0xf289,
            Mobile = 0xf10B,
            MobilePhone = 0xf10B,
            Modx = 0xf285,
            Money = 0xf0D6,
            MoonO = 0xf186,
            MortarBoard = 0xf19D,
            Motorcycle = 0xf21C,
            MousePointer = 0xf245,
            Music = 0xf001,
            Navicon = 0xf0C9,
            Neuter = 0xf22C,
            NewspaperO = 0xf1Ea,
            ObjectGroup = 0xf247,
            ObjectUngroup = 0xf248,
            Odnoklassniki = 0xf263,
            OdnoklassnikiSquare = 0xf264,
            Opencart = 0xf23D,
            Openid = 0xf19B,
            Opera = 0xf26A,
            OptinMonster = 0xf23C,
            Outdent = 0xf03B,
            Pagelines = 0xf18C,
            PaintBrush = 0xf1Fc,
            PaperPlane = 0xf1D8,
            PaperPlaneO = 0xf1D9,
            Paperclip = 0xf0C6,
            Paragraph = 0xf1Dd,
            Paste = 0xf0Ea,
            Pause = 0xf04C,
            PauseCircle = 0xf28B,
            PauseCircleO = 0xf28C,
            Paw = 0xf1B0,
            Paypal = 0xf1Ed,
            Pencil = 0xf040,
            PencilSquare = 0xf14B,
            PencilSquareO = 0xf044,
            Percent = 0xf295,
            Phone = 0xf095,
            PhoneSquare = 0xf098,
            Photo = 0xf03E,
            PictureO = 0xf03E,
            PieChart = 0xf200,
            PiedPiper = 0xf2Ae,
            PiedPiperAlt = 0xf1A8,
            PiedPiperPp = 0xf1A7,
            Pinterest = 0xf0D2,
            PinterestP = 0xf231,
            PinterestSquare = 0xf0D3,
            Plane = 0xf072,
            Play = 0xf04B,
            PlayCircle = 0xf144,
            PlayCircleO = 0xf01D,
            Plug = 0xf1E6,
            Plus = 0xf067,
            PlusCircle = 0xf055,
            PlusSquare = 0xf0Fe,
            PlusSquareO = 0xf196,
            Podcast = 0xf2Ce,
            PowerOff = 0xf011,
            Print = 0xf02F,
            ProductHunt = 0xf288,
            PuzzlePiece = 0xf12E,
            Qq = 0xf1D6,
            Qrcode = 0xf029,
            Question = 0xf128,
            QuestionCircle = 0xf059,
            QuestionCircleO = 0xf29C,
            Quora = 0xf2C4,
            QuoteLeft = 0xf10D,
            QuoteRight = 0xf10E,
            Ra = 0xf1D0,
            Random = 0xf074,
            Ravelry = 0xf2D9,
            Rebel = 0xf1D0,
            Recycle = 0xf1B8,
            Reddit = 0xf1A1,
            RedditAlien = 0xf281,
            RedditSquare = 0xf1A2,
            Refresh = 0xf021,
            Registered = 0xf25D,
            Remove = 0xf00D,
            Renren = 0xf18B,
            Reorder = 0xf0C9,
            Repeat = 0xf01E,
            Reply = 0xf112,
            ReplyAll = 0xf122,
            Resistance = 0xf1D0,
            Retweet = 0xf079,
            Rmb = 0xf157,
            Road = 0xf018,
            Rocket = 0xf135,
            RotateLeft = 0xf0E2,
            RotateRight = 0xf01E,
            Rouble = 0xf158,
            Rss = 0xf09E,
            RssSquare = 0xf143,
            Rub = 0xf158,
            Ruble = 0xf158,
            Rupee = 0xf156,
            S15 = 0xf2Cd,
            Safari = 0xf267,
            Save = 0xf0C7,
            Scissors = 0xf0C4,
            Scribd = 0xf28A,
            Search = 0xf002,
            SearchMinus = 0xf010,
            SearchPlus = 0xf00E,
            Sellsy = 0xf213,
            Send = 0xf1D8,
            SendO = 0xf1D9,
            Server = 0xf233,
            Share = 0xf064,
            ShareAlt = 0xf1E0,
            ShareAltSquare = 0xf1E1,
            ShareSquare = 0xf14D,
            ShareSquareO = 0xf045,
            Shekel = 0xf20B,
            Sheqel = 0xf20B,
            Shield = 0xf132,
            Ship = 0xf21A,
            Shirtsinbulk = 0xf214,
            ShoppingBag = 0xf290,
            ShoppingBasket = 0xf291,
            ShoppingCart = 0xf07A,
            Shower = 0xf2Cc,
            SignIn = 0xf090,
            SignLanguage = 0xf2A7,
            SignOut = 0xf08B,
            Signal = 0xf012,
            Signing = 0xf2A7,
            Simplybuilt = 0xf215,
            Sitemap = 0xf0E8,
            Skyatlas = 0xf216,
            Skype = 0xf17E,
            Slack = 0xf198,
            Sliders = 0xf1De,
            Slideshare = 0xf1E7,
            SmileO = 0xf118,
            Snapchat = 0xf2Ab,
            SnapchatGhost = 0xf2Ac,
            SnapchatSquare = 0xf2Ad,
            SnowflakeO = 0xf2Dc,
            SoccerBallO = 0xf1E3,
            Sort = 0xf0Dc,
            SortAlphaAsc = 0xf15D,
            SortAlphaDesc = 0xf15E,
            SortAmountAsc = 0xf160,
            SortAmountDesc = 0xf161,
            SortAsc = 0xf0De,
            SortDesc = 0xf0Dd,
            SortDown = 0xf0Dd,
            SortNumericAsc = 0xf162,
            SortNumericDesc = 0xf163,
            SortUp = 0xf0De,
            Soundcloud = 0xf1Be,
            SpaceShuttle = 0xf197,
            Spinner = 0xf110,
            Spoon = 0xf1B1,
            Spotify = 0xf1Bc,
            Square = 0xf0C8,
            SquareO = 0xf096,
            StackExchange = 0xf18D,
            StackOverflow = 0xf16C,
            Star = 0xf005,
            StarHalf = 0xf089,
            StarHalfEmpty = 0xf123,
            StarHalfFull = 0xf123,
            StarHalfO = 0xf123,
            StarO = 0xf006,
            Steam = 0xf1B6,
            SteamSquare = 0xf1B7,
            StepBackward = 0xf048,
            StepForward = 0xf051,
            Stethoscope = 0xf0F1,
            StickyNote = 0xf249,
            StickyNoteO = 0xf24A,
            Stop = 0xf04D,
            StopCircle = 0xf28D,
            StopCircleO = 0xf28E,
            StreetView = 0xf21D,
            Strikethrough = 0xf0Cc,
            Stumbleupon = 0xf1A4,
            StumbleuponCircle = 0xf1A3,
            Subscript = 0xf12C,
            Subway = 0xf239,
            Suitcase = 0xf0F2,
            SunO = 0xf185,
            Superpowers = 0xf2Dd,
            Superscript = 0xf12B,
            Support = 0xf1Cd,
            Table = 0xf0Ce,
            Tablet = 0xf10A,
            Tachometer = 0xf0E4,
            Tag = 0xf02B,
            Tags = 0xf02C,
            Tasks = 0xf0Ae,
            Taxi = 0xf1Ba,
            Telegram = 0xf2C6,
            Television = 0xf26C,
            TencentWeibo = 0xf1D5,
            Terminal = 0xf120,
            TextHeight = 0xf034,
            TextWidth = 0xf035,
            Th = 0xf00A,
            ThLarge = 0xf009,
            ThList = 0xf00B,
            Themeisle = 0xf2B2,
            Thermometer = 0xf2C7,
            Thermometer0 = 0xf2Cb,
            Thermometer1 = 0xf2Ca,
            Thermometer2 = 0xf2C9,
            Thermometer3 = 0xf2C8,
            Thermometer4 = 0xf2C7,
            ThermometerEmpty = 0xf2Cb,
            ThermometerFull = 0xf2C7,
            ThermometerHalf = 0xf2C9,
            ThermometerQuarter = 0xf2Ca,
            ThermometerThreeQuarters = 0xf2C8,
            ThumbTack = 0xf08D,
            ThumbsDown = 0xf165,
            ThumbsODown = 0xf088,
            ThumbsOUp = 0xf087,
            ThumbsUp = 0xf164,
            Ticket = 0xf145,
            TimesCircle = 0xf057,
            TimesCircleO = 0xf05C,
            TimesRectangle = 0xf2D3,
            TimesRectangleO = 0xf2D4,
            Tint = 0xf043,
            ToggleDown = 0xf150,
            ToggleLeft = 0xf191,
            ToggleOff = 0xf204,
            ToggleOn = 0xf205,
            ToggleRight = 0xf152,
            ToggleUp = 0xf151,
            Trademark = 0xf25C,
            Train = 0xf238,
            Transgender = 0xf224,
            TransgenderAlt = 0xf225,
            Trash = 0xf1F8,
            TrashO = 0xf014,
            Tree = 0xf1Bb,
            Trello = 0xf181,
            Tripadvisor = 0xf262,
            Trophy = 0xf091,
            Truck = 0xf0D1,
            Try = 0xf195,
            Tty = 0xf1E4,
            Tumblr = 0xf173,
            TumblrSquare = 0xf174,
            TurkishLira = 0xf195,
            Tv = 0xf26C,
            Twitch = 0xf1E8,
            Twitter = 0xf099,
            TwitterSquare = 0xf081,
            Umbrella = 0xf0E9,
            Underline = 0xf0Cd,
            Undo = 0xf0E2,
            UniversalAccess = 0xf29A,
            University = 0xf19C,
            Unlink = 0xf127,
            Unlock = 0xf09C,
            UnlockAlt = 0xf13E,
            Unsorted = 0xf0Dc,
            Upload = 0xf093,
            Usb = 0xf287,
            Usd = 0xf155,
            User = 0xf007,
            UserCircle = 0xf2Bd,
            UserCircleO = 0xf2Be,
            UserMd = 0xf0F0,
            UserO = 0xf2C0,
            UserPlus = 0xf234,
            UserSecret = 0xf21B,
            UserTimes = 0xf235,
            Users = 0xf0C0,
            Vcard = 0xf2Bb,
            VcardO = 0xf2Bc,
            Venus = 0xf221,
            VenusDouble = 0xf226,
            VenusMars = 0xf228,
            Viacoin = 0xf237,
            Viadeo = 0xf2A9,
            ViadeoSquare = 0xf2Aa,
            VideoCamera = 0xf03D,
            Vimeo = 0xf27D,
            VimeoSquare = 0xf194,
            Vine = 0xf1Ca,
            Vk = 0xf189,
            VolumeControlPhone = 0xf2A0,
            VolumeDown = 0xf027,
            VolumeOff = 0xf026,
            VolumeUp = 0xf028,
            Warning = 0xf071,
            Wechat = 0xf1D7,
            Weibo = 0xf18A,
            Weixin = 0xf1D7,
            Whatsapp = 0xf232,
            Wheelchair = 0xf193,
            WheelchairAlt = 0xf29B,
            Wifi = 0xf1Eb,
            WikipediaW = 0xf266,
            WindowClose = 0xf2D3,
            WindowCloseO = 0xf2D4,
            WindowMaximize = 0xf2D0,
            WindowMinimize = 0xf2D1,
            WindowRestore = 0xf2D2,
            Windows = 0xf17A,
            Won = 0xf159,
            Wordpress = 0xf19A,
            Wpbeginner = 0xf297,
            Wpexplorer = 0xf2De,
            Wpforms = 0xf298,
            Wrench = 0xf0Ad,
            Xing = 0xf168,
            XingSquare = 0xf169,
            YCombinator = 0xf23B,
            YCombinatorSquare = 0xf1D4,
            Yahoo = 0xf19E,
            Yc = 0xf23B,
            YcSquare = 0xf1D4,
            Yelp = 0xf1E9,
            Yen = 0xf157,
            Yoast = 0xf2B1,
            Youtube = 0xf167,
            YoutubePlay = 0xf16A,
            YoutubeSquare = 0xf166
        }
    }

    public static class FontAwesomeExtensions
    {
        public static Bitmap StackWith(this Bitmap backgroundImage, FontAwesome4.Properties foregroundImage)
        {
            var bitmap = backgroundImage;
            Graphics g = Graphics.FromImage(backgroundImage);
            g.DrawImage(FontAwesome4.Instance.GetImage(foregroundImage), new Point(0, 0));
            return bitmap;
        }

        public static Bitmap StackWith(this Bitmap backgroundImage, Bitmap foregroundImage)
        {
            var bitmap = backgroundImage;
            Graphics g = Graphics.FromImage(backgroundImage);
            g.DrawImage(foregroundImage, new Point(0, 0));
            return bitmap;
        }

        public static Bitmap AsImage(this FontAwesome4.Type type, FontAwesome4.Properties fontProperties = null)
        {
            if (fontProperties == null)
                fontProperties = FontAwesome4.Properties.Get(type);
            else
                fontProperties.Type = type;
            return fontProperties.AsImage();
        }

        public static Bitmap AsImage(this FontAwesome4.Properties fontProperties)
        {
            return FontAwesome4.Instance.GetImage(fontProperties);
        }

        public static Icon AsIcon(this FontAwesome4.Type type, FontAwesome4.Properties fontProperties = null)
        {
            if (fontProperties == null)
                fontProperties = FontAwesome4.Properties.Get(type);
            else
                fontProperties.Type = type;
            return fontProperties.AsIcon();
        }

        public static Icon AsIcon(this FontAwesome4.Properties fontProperties)
        {
            return FontAwesome4.Instance.GetIcon(fontProperties);
        }
    }
}