using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace polyhedraV3{
	public class Face{
		
		private int data;
		private Tuple<int,int, int>[] sides = new Tuple<int,int, int>[0];
		
		public Face(){}
		public Face(int d, Tuple<int,int,int>[] mysides){
			this.data = d;
			this.sides = mysides;
		}
		
		public void Add(int v, int e, int f){
			this.Add(Tuple.Create(v,e,f));
		}
		
		public void Add(Tuple<int, int, int> ev){
			Tuple<int,int, int>[] newsides = new Tuple<int, int, int>[sides.Length+1];
			sides.CopyTo(newsides,0);
			newsides[sides.Length] = ev;
			sides = newsides;
			
		}
		
		public Tuple<int,int,int> this[int index]{
			get{
				return sides[index];
			}
		}
		public int Count(){
			return sides.Length;
		}
		public List<Tuple<int,int,int>> ToList{
			get{
				return sides.ToList();
			}
		}
		
		public int Data{
			get{
				return this.data;
			}
		}
		public List<int> Vertices {
			get{
				return sides.ToList().Select(s=>s.Item1).ToList();
			}
		}
		public List<int> Edges {
			get {
				return sides.ToList().Select(s=>s.Item2).ToList();
			}
		}
		
		public F GetData<F,E,V>(FaceHedron<F,E,V> parent){
			return parent.FaceData[this.data];
		}
		public V GetVertex<F,E,V>(int index, FaceHedron<F,E,V> parent){
			return parent.VertexData[sides[index].Item1];
		}
		public List<V> GetVertices<F,E,V>(FaceHedron<F,E,V> parent){
			return sides.Select(t=>parent.VertexData[t.Item1]).ToList();
		}
		
		public E GetEdge<F,E,V>(int index, FaceHedron<F,E,V> parent){
			return parent.EdgeData[sides[index].Item2];
		}
		public List<E> GetEdges<F,E,V>(FaceHedron<F,E,V> parent){
			return sides.Select(t=>parent.EdgeData[t.Item2]).ToList();
		}
		
		public F GetNeighbor<F,E,V>(int index, FaceHedron<F,E,V> parent){
			return parent.FaceData[sides[index].Item3];
		}
		public List<F> GetNeighbors<F,E,V>(FaceHedron<F,E,V> parent){
			return sides.Select(t=>parent.FaceData[t.Item3]).ToList();
		}
	}
}

