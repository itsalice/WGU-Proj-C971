using SQLite;
using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentApp.Services
{
    public class TermService
    {
        static SQLiteAsyncConnection db;

        public TermService()
        {
        }

        static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            db = new DataService().GetConnection();

            await db.CreateTableAsync<Term>();
        }

        public static async Task AddTerm(string name, DateTime start, DateTime end)
        {
            await Init();

            var term = new Term
            {
                Name = name,
                Start = start,
                End = end
            };

            await db.InsertAsync(term);
        }

        public static async Task UpdateTerm(int id, string name, DateTime start, DateTime end)
        {
            await Init();

            var term = await db.Table<Term>().FirstOrDefaultAsync(t => t.Id == id);
            if (term != null)
            {
                term.Name = name;
                term.Start = start;
                term.End = end;
            }

            await db.UpdateAsync(term);
        }

        public static async Task RemoveTerm(int id)
        {
            await Init();

            await db.DeleteAsync<Term>(id);
        }

        public static async Task<Term> GetTerm(int id)
        {
            await Init();

            return await db.Table<Term>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public static async Task<List<Term>> GetTermsAsync()
        {
            await Init();

            return await db.Table<Term>().OrderBy(x => x.Start).ToListAsync();
        }
    }
}
