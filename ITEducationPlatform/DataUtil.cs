using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace ITEducationPlatform
{
    public class DataUtil
    {
        public static bool IsRecordExist(string user)
        {
            string query = string.Format("select count(*) from TB_IT_EDU_HISTORY where h_user = N'{0}'", user);
            int result = (int)DataService.GetInstance().ExecuteScalar(query);

            return result > 0 ? true : false;
        }

        public static string GetQuestion(int id, string mode)
        {
            string query = mode == "jp" ? string.Format("select q_questionjp from TB_IT_EDU_QUESTION where q_id = {0}", id) : string.Format("select q_question from TB_IT_EDU_QUESTION where q_id = {0}", id);
            return DataService.GetInstance().ExecuteScalar(query).ToString().Trim();
        }

        public static List<string> GetAnswerList(int id, string mode, int take)
        {
            List<string> list = new List<string>();

            string query = mode == "jp" ? string.Format("select a_answerjp from TB_IT_EDU_ANSWER where a_questionid = {0}", id) : string.Format("select a_answer from TB_IT_EDU_ANSWER where a_questionid = {0}", id);

            using (IDataReader reader = DataService.GetInstance().ExecuteReader(query))
            {
                while (reader.Read())
                    list.Add(reader.GetString(0).Trim());
            }

            Random rnd = new Random();
            list = list.OrderBy(x => rnd.Next()).Take(take).ToList();

            return list;
        }

        public static bool IsAnswerCorrect(int questionId, string answer, string mode, bool IsChecked)
        {
            string query = mode == "jp" ? string.Format("select a_correct from TB_IT_EDU_ANSWER where a_answerjp = N'{0}' and a_questionid = {1}", answer, questionId)
                : string.Format("select a_correct from TB_IT_EDU_ANSWER where a_answer like N'{0}%' and a_questionid = {1}", answer, questionId);

            string result = DataService.GetInstance().ExecuteScalar(query).ToString();

            if (result == "True" && IsChecked)
                return true;
            if (result == "False" && !IsChecked)
                return true;

            return false;
        }

        public static string GetCategory(int questionId)
        {
            string query = string.Format("select q_type from TB_IT_EDU_QUESTION where q_id = {0}", questionId);
            return DataService.GetInstance().ExecuteScalar(query).ToString();
        }

        public static void SaveRecord(string user, string category, int questionId, string correct, string company)
        {
            string query = string.Format("insert into TB_IT_EDU_REPORT(r_user, r_category, r_questionid, r_correct, r_company) values (N'{0}', '{1}', {2}, '{3}', '{4}')", user, category, questionId, correct, company);
            DataService.GetInstance().ExecuteNonQuery(query);
        }

        public static void DeleteRecord(string user)
        {
            string query = string.Format("delete from TB_IT_EDU_REPORT where r_user = N'{0}'", user);
            DataService.GetInstance().ExecuteNonQuery(query);
        }

        public static void SaveHistory(string user, string form)
        {
            string query = string.Format("update TB_IT_EDU_HISTORY set h_answered = 'True' where h_user = N'{0}' and h_form = '{1}'", user, form);
            DataService.GetInstance().ExecuteNonQuery(query);
        }

        public static int GetScore(string user)
        {
            //string query = string.Format("select count(*) from TB_IT_EDU_REPORT where r_correct = 'Yes' and r_user = N'{0}'", user);
            string query = string.Format(@"WITH t as (SELECT TOP 8 [r_id],[r_user],[r_category],[r_questionId],[r_correct],[r_company],[r_insertedAt]
  FROM [KDTHK_DB].[dbo].[TB_IT_EDU_REPORT]
  WHERE r_user = N'{0}'
  ORDER BY r_id DESC
)
select count(*) from t where r_correct = 'Yes'", user);
            return (int)DataService.GetInstance().ExecuteScalar(query);
        }

        public static bool isRecordSumitted()
        {
            string query = string.Format("select count(*) from TB_IT_EDU_RECORD where r_user = N'{0}'", GlobalService.User);
            int result = (int)DataService.GetInstance().ExecuteScalar(query);
            return result == 0 ? false : true;
        }
    }
}
