using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Spotivy_Nick_en_Niels
{
    internal class Data
    {
        public static void AddStandardData(Music MusicCollection)
        {
            Artist Artist1 = new Artist("Eminem", "Artist1Password");
            Artist Artist2 = new Artist("Imagine Dragons", "Artist1Password");
            Song Song1 = new Song("'Till I Collapse", Artist1, "'Cause sometimes you just feel tired, you feel weak\r\nAnd when you feel weak you feel like you want to just give up\r\n\r\nBut you gotta search within you, you gotta find that inner strength\r\nAnd just pull that shit out of you and get that motivation to not give up\r\nAnd not be a quitter, no matter how bad you want to just fall flat on your face and collapse\r\n\r\n'Til I collapse I'm spilling these raps long as you feel 'em\r\n'Til the day that I drop you'll never say that I'm not killing 'em\r\n'Cause when I am not then I'm a stop pinning them\r\nAnd I am not hip-hop and I'm just not Eminem\r\nSubliminal thoughts when I'm stop sending them\r\nWomen are caught in webs spin and hock venom\r\nAdrenaline shots of penicillin could not get the illin' to stop\r\nAmoxicillin is just not real enough\r\nThe criminal cop killing hip-hop filling a\r\nMinimal swap to cop millions of Pac listeners\r\nYou're coming with me, feel it or not\r\nYou're gonna fear it like I showed you the spirit of god lives in us\r\nYou hear it a lot, lyrics that shock, is it a miracle\r\nOr am I just a product of pop fizzing up\r\nFor shizzle my whizzle this is the plot listen up\r\nYou Bizzles forgot Slizzle does not give a fuck\r\n\r\n'Til the roof comes off, till the lights go out\r\n'Til my legs give out, can't shut my mouth\r\n'Til the smoke clears out and my high perhaps\r\nI'm a rip this shit till my bone collapse\r\n\r\n'Til the roof comes off, till the lights go out\r\n'Til my legs give out, can't shut my mouth\r\n'Til the smoke clears out and my high perhaps\r\nI'm a rip this shit till my bone collapse\r\n\r\nMusic is like magic there's a certain feeling you get\r\nWhen you're real and you spit and people are feeling your shit\r\nThis is your moment and every single minute you spittin'\r\nTrying to hold onto it 'cause you may never get it again\r\nSo while you're in it try to get as much shit as you can\r\nAnd when your run is over just admit when it's at its end\r\n'Cause I'm at the end of my wits with half the shit that gets in\r\nI got a list, here's the order of my list that it's in;\r\nIt goes, Reggie, Jay-Z, Tupac and Biggie\r\nAndre from Outkast, Jada, Kurupt, Nas and then me\r\nBut in this industry I'm the cause of a lot of envy\r\nSo when I'm not put on this list the shit does not offend me\r\nThat's why you see me walk around like nothing's bothering me\r\nEven though half you people got a fuckin' problem with me\r\nYou hate it but you know respect you've got to give me\r\nThe press's wet dream like Bobby and Whitney, Nate hit me\r\n'Til the roof comes off, till the lights go out\r\n'Til my legs give out, can't shut my mouth\r\n'Til the smoke clears out and my high perhaps\r\nI'm a rip this shit till my bone collapse\r\n\r\n'Til the roof comes off, till the lights go out\r\n'Til my legs give out, can't shut my mouth\r\n'Til the smoke clears out and my high perhaps\r\nI'm a rip this shit till my bone collapse\r\n\r\nSoon as a verse starts I eat at an MC's heart\r\nWhat is he thinking? Enough to not go against me, smart\r\nAnd its absurd how people hang on every word\r\nI'll probably never get the props I feel I ever deserve\r\nBut I'll never be served my spot is forever reserved\r\nIf I ever leave earth that would be the death of me first\r\n'Cause in my heart of hearts I know nothing could ever be worse\r\nThat's why I'm clever when I put together every verse\r\nMy thoughts are sporadic, I act like I'm an addict\r\nI rap like I'm addicted to smack like I'm Kim Mathers\r\nBut I don't want to go forth and back in constant battles\r\nThe fact is I would rather sit back and bomb some rappers'\r\nSo this is like a full blown attack I'm launching at 'em\r\nThe track is on some battling raps who want some static\r\n'Cause I don't really think that the fact that I'm Slim matters\r\nA plaque of platinum status is whack if I'm not the baddest\r\n\r\n'Til the roof comes off, till the lights go out\r\n'Til my legs give out, can't shut my mouth\r\n'Til the smoke clears out and my high perhaps\r\nI'm a rip this shit till my bone collapse\r\n\r\n'Til the roof comes off, till the lights go out\r\n'Til my legs give out, can't shut my mouth\r\n'Til the smoke clears out and my high perhaps\r\nI'm a rip this shit till my bone collapse\r\n\r\nUntil the roof (Until the roof)\r\nThe roof comes off (The roof comes off)\r\nUntil my legs (Until my legs)\r\nGive out from underneath me (Underneath me, I)\r\n\r\nI will not fall\r\nI will stand tall\r\nFeels like no one can beat me", MusicCollection);
            Song Song2 = new Song("Curse", Artist2, "She barely knew your name\r\n\r\nHe was just a city\r\nShe's just a dirt road\r\nBut that never meant a thing\r\nRunning from the country\r\nShe needed out\r\nBut he held that diamond ring\r\n\r\nCause I can't sit oh I can't talk\r\nI gotta leave this town\r\nAnd run to you\r\nCurse these nights\r\nThat speak your name\r\nI gotta leave this town\r\nAnd come to you\r\nBreak (oh)\r\nThis (oh)\r\nCurse (oh)\r\nHa ha ha\r\nBreak (oh)\r\nThis (oh)\r\nCurse (oh)\r\nShe barely knew your name\r\n\r\nHe was such a worry\r\nNo need to hurry\r\nThose streets are slick at night\r\nShe would never listen\r\nShe left it all\r\nAnd headed towards the light\r\nOh your eyes look tired\r\nBut love was all she knew\r\nScreaming out in agony\r\nShe gave her life for you\r\n\r\nCause I can't sit oh I can't talk\r\nI gotta leave this town\r\nAnd run to you\r\nCurse these nights\r\nThat speak your name\r\nI gotta leave this town\r\nAnd come to you\r\nBreak (oh)\r\nThis (oh)\r\nCurse (oh)\r\nHa ha ha\r\nBreak (oh)\r\nThis (oh)\r\nCurse (oh)\r\nShe barely knew your name\r\n\r\n(instumental part)\r\n\r\noooooohwooo ooh woo ohwoowo wowowo\r\noooooohwooo ooh woo ohwoowo wowowo\r\n\r\nCause I can't sit oh I can't talk\r\nI gotta leave this town\r\nAnd run to you\r\nCurse these nights\r\nThat speak your name\r\nI gotta leave this town\r\nAnd come to you\r\nBreak (oh)\r\nThis (oh)\r\nCurse (oh)\r\nHa ha ha\r\nBreak (oh)\r\nThis (oh)\r\nCurse (oh)\r\nShe barely knew your name", MusicCollection);
        }
    }

    internal class Music
    {
        private List<Song> music = new List<Song>();

        public void AddSong(Song song)
        {
            music.Add(song);
        }

        public List<Song> GetListOfMusic()
        {
            return music;
        }
    }

    internal class Artists
    {
        private List<Artist> artists = new List<Artist>();

        public void AddArtist(Artist artist)
        {
            artists.Add(artist);
        }

        public List<Artist> GetListOfMusic()
        {
            return artists;
        }
    }
}
