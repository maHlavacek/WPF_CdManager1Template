﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace CdManager.Model
{
    /// <summary>
    /// Repository als Singleton, damit die Daten aus dem CSV-File nur einmal gelesen werden!
    /// </summary>
    public class Repository
    {
        private const string fileName = "AlbumCds.csv";

        private static Repository instance;

        List<Cd> cds;

        private Repository()
        {
            cds = new List<Cd>();
            LoadCdsFromCsv();
        }

        public static Repository GetInstance()
        {
            if (instance == null)
                instance = new Repository();

            return instance;
        }

        /// <summary>
        /// Lädt die Daten vom csv-File in die Collection
        /// </summary>
        private void LoadCdsFromCsv()
        {
            string[][] cdCsv = fileName.ReadStringMatrixFromCsv(true);
            cds = cdCsv.GroupBy(line => new { Artist = line[0], Album = line[3] }).Select(x =>
                new Cd
                {
                    AlbumTitle = x.Key.Album,
                    Artist = x.Key.Artist,
                    Tracks = cdCsv.Where(tr => tr[3] == x.Key.Album).Select(cdTrack =>
                        new Track
                        {
                            Title = cdTrack[1],
                            Year = Convert.ToInt32(cdTrack[2]),
                            SongWriter = cdTrack[4],
                            LeadVocals = cdTrack[5]
                        }).ToList()
                }).ToList();
        }

        /// <summary>
        /// Neue Cd in die Collection einfügen
        /// </summary>
        /// <param name="cd"></param>
        public void AddCd(Cd cd)
        {
            cds.Add(cd);
        }

        /// <summary>
        /// Liefert eine (neue!) Liste aller Cds
        /// Entkoppelt die zurückgelieferte Collektion von der Collection im Repository
        /// Beachte! Die Objekte selbst sind jedoch noch dieselben (clonen wäre erforderlich)!
        /// </summary>
        /// <returns></returns>
        public List<Cd> GetAllCds()
        {
            return cds.OrderBy(x => x.AlbumTitle).ToList(); //Erstellt neue Liste!
        }
    }
}
